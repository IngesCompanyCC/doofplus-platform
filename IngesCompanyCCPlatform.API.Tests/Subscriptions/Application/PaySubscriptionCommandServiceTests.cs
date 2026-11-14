using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Subscriptions.Application.OutboundServices.PaymentGateway;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Tests.Subscriptions.Application;

/// <summary>
///     Tests of the payment use case of <see cref="SubscriptionCommandService" />, with in-memory test doubles.
/// </summary>
public class PaySubscriptionCommandServiceTests
{
    private readonly FakeSubscriptionRepository _subscriptions = new();
    private readonly FakePlanRepository _plans = new();
    private readonly FakeUnitOfWork _unitOfWork = new();
    private readonly FakePaymentGateway _gateway = new();

    private SubscriptionCommandService Service => new(_subscriptions, _plans, _gateway, _unitOfWork);

    private static PaySubscriptionCommand Pay => new(new SubscriptionId(0), "7B4E2A1C9D8F4A6B");

    [Fact]
    public async Task AnApprovedPaymentShouldActivateAPendingSubscriptionAndBeSaved()
    {
        _subscriptions.Stored = SubscriptionTestData.PendingSubscription();
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _gateway.Result = PaymentResult.ApprovedWith("993201", "Approved and completed successfully");

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(ESubscriptionStatus.Active, result.Value!.Status);
        var payment = Assert.Single(result.Value.Payments);
        Assert.Equal(EPaymentStatus.Approved, payment.Status);
        Assert.Equal("993201", payment.GatewayTransactionId);
        Assert.Equal(199m, payment.Amount);
        Assert.Equal(1, _unitOfWork.Completed);
    }

    [Fact]
    public async Task AnAnnualSubscriptionShouldBeChargedTheAnnualPrice()
    {
        _subscriptions.Stored = SubscriptionTestData.PendingSubscription(EBillingCycle.Annual);
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _gateway.Result = PaymentResult.ApprovedWith("993201", "Approved");

        await Service.Handle(Pay, CancellationToken.None);

        Assert.Equal(new Money(1990m, "USD"), _gateway.ChargedAmount);
    }

    [Fact]
    public async Task AnApprovedPaymentShouldRenewAnActiveSubscription()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        var currentEnd = subscription.CurrentPeriodEnd!.Value;
        _subscriptions.Stored = subscription;
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _gateway.Result = PaymentResult.ApprovedWith("993202", "Approved");

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(currentEnd.AddMonths(1), result.Value!.CurrentPeriodEnd);
    }

    [Fact]
    public async Task ARejectedPaymentShouldBeRecordedAndAnswerPaymentRejected()
    {
        _subscriptions.Stored = SubscriptionTestData.PendingSubscription();
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _gateway.Result = PaymentResult.RejectedWith("Insufficient funds");

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SubscriptionsError.PaymentRejected, result.Error);
        Assert.Equal(ESubscriptionStatus.Pending, _subscriptions.Stored!.Status);
        Assert.Equal(EPaymentStatus.Rejected, Assert.Single(_subscriptions.Stored.Payments).Status);
        Assert.Equal(1, _unitOfWork.Completed);
    }

    [Fact]
    public async Task AnUnavailableGatewayShouldAnswerUnavailableAndSaveNothing()
    {
        _subscriptions.Stored = SubscriptionTestData.PendingSubscription();
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _gateway.Result = PaymentResult.UnavailableWith("Payment gateway is not available");

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SubscriptionsError.PaymentGatewayUnavailable, result.Error);
        Assert.Empty(_subscriptions.Stored!.Payments);
        Assert.Equal(0, _unitOfWork.Completed);
    }

    [Fact]
    public async Task AMissingSubscriptionShouldAnswerNotFoundWithoutCallingTheGateway()
    {
        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.Equal(SubscriptionsError.SubscriptionNotFound, result.Error);
        Assert.Equal(0, _gateway.Calls);
    }

    [Fact]
    public async Task ACancelledSubscriptionShouldNotBePaid()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        subscription.Cancel();
        _subscriptions.Stored = subscription;
        _plans.Stored = SubscriptionTestData.StandardLabPlan();

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.Equal(SubscriptionsError.InvalidSubscriptionState, result.Error);
        Assert.Equal(0, _gateway.Calls);
    }

    [Fact]
    public async Task AMissingPlanShouldAnswerPlanNotFound()
    {
        _subscriptions.Stored = SubscriptionTestData.PendingSubscription();

        var result = await Service.Handle(Pay, CancellationToken.None);

        Assert.Equal(SubscriptionsError.PlanNotFound, result.Error);
        Assert.Equal(0, _gateway.Calls);
    }

    [Fact]
    public async Task ASecondSubscriptionOfTheSameOrganizationShouldBeRejected()
    {
        _plans.Stored = SubscriptionTestData.StandardLabPlan();
        _subscriptions.OrganizationHasCurrentSubscription = true;
        var command = new CreateSubscriptionCommand(new OrganizationId(1), new PlanId(1), EBillingCycle.Monthly,
            new PaymentCard("Visa", "4242", "08/2028"), new BillingEmail("billing@andinos.com.pe"));

        var result = await Service.Handle(command, CancellationToken.None);

        Assert.Equal(SubscriptionsError.AlreadySubscribed, result.Error);
        Assert.Equal(0, _unitOfWork.Completed);
    }

    private sealed class FakeSubscriptionRepository : ISubscriptionRepository
    {
        public Subscription? Stored { get; set; }
        public bool OrganizationHasCurrentSubscription { get; set; }

        public Task AddAsync(Subscription entity, CancellationToken cancellationToken = default)
        {
            Stored = entity;
            return Task.CompletedTask;
        }

        public Task<Subscription?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);

        public Task<Subscription?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);

        public void Update(Subscription entity) => Stored = entity;

        public void Remove(Subscription entity) => Stored = null;

        public Task<IEnumerable<Subscription>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Subscription>>(Stored is null ? [] : [Stored]);

        public Task<IEnumerable<Subscription>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<IEnumerable<Subscription>> FindAllByOrganizationIdAsNoTrackingAsync(OrganizationId organizationId,
            CancellationToken cancellationToken = default) => ListAsync(cancellationToken);

        public Task<bool> ExistsByOrganizationIdAndStatusInAsync(OrganizationId organizationId,
            IEnumerable<ESubscriptionStatus> statuses, CancellationToken cancellationToken = default) =>
            Task.FromResult(OrganizationHasCurrentSubscription);
    }

    private sealed class FakePlanRepository : IPlanRepository
    {
        public Plan? Stored { get; set; }

        public Task AddAsync(Plan entity, CancellationToken cancellationToken = default)
        {
            Stored = entity;
            return Task.CompletedTask;
        }

        public Task<Plan?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);

        public Task<Plan?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);

        public void Update(Plan entity) => Stored = entity;

        public void Remove(Plan entity) => Stored = null;

        public Task<IEnumerable<Plan>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<Plan>>(Stored is null ? [] : [Stored]);

        public Task<IEnumerable<Plan>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<bool> ExistsByPlanKeyAsync(PlanKey planKey, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored?.PlanKey == planKey);
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public int Completed { get; private set; }

        public Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            Completed++;
            return Task.CompletedTask;
        }
    }

    private sealed class FakePaymentGateway : IPaymentGateway
    {
        public PaymentResult Result { get; set; } = PaymentResult.ApprovedWith("1", "Approved");
        public int Calls { get; private set; }
        public Money? ChargedAmount { get; private set; }

        public Task<PaymentResult> AuthorizeAsync(Money amount, string cardToken,
            CancellationToken cancellationToken = default)
        {
            Calls++;
            ChargedAmount = amount;
            return Task.FromResult(Result);
        }
    }
}
