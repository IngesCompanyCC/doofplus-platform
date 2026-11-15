using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Subscriptions.Domain;

/// <summary>
///     Unit tests of the Subscription aggregate (US51, US52, US53).
/// </summary>
public class SubscriptionTests
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

    [Fact]
    public void NewSubscriptionShouldBePendingWithAutoRenewAndNoPeriod()
    {
        var subscription = SubscriptionTestData.PendingSubscription();

        Assert.Equal(ESubscriptionStatus.Pending, subscription.Status);
        Assert.True(subscription.AutoRenew);
        Assert.True(subscription.IsCurrent);
        Assert.Null(subscription.CurrentPeriodStart);
        Assert.Null(subscription.CurrentPeriodEnd);
        Assert.Empty(subscription.Payments);
    }

    [Fact]
    public void ActivateShouldStartAMonthlyPeriod()
    {
        var subscription = SubscriptionTestData.PendingSubscription();

        subscription.Activate();

        Assert.Equal(ESubscriptionStatus.Active, subscription.Status);
        Assert.Equal(Today, subscription.CurrentPeriodStart);
        Assert.Equal(Today.AddMonths(1), subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void ActivateShouldStartAnAnnualPeriod()
    {
        var subscription = SubscriptionTestData.PendingSubscription(EBillingCycle.Annual);

        subscription.Activate();

        Assert.Equal(Today.AddYears(1), subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void ActivateShouldFailWhenTheSubscriptionIsNotPending()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();

        var exception = Assert.Throws<InvalidOperationException>(subscription.Activate);

        Assert.Equal("subscription.error.state.notPending", exception.Message);
    }

    [Fact]
    public void RenewShouldStartTheNextPeriodWhereTheCurrentOneEnds()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        var currentEnd = subscription.CurrentPeriodEnd!.Value;

        subscription.Renew();

        Assert.Equal(currentEnd, subscription.CurrentPeriodStart);
        Assert.Equal(currentEnd.AddMonths(1), subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void RenewShouldFailWhenTheSubscriptionIsPending()
    {
        var subscription = SubscriptionTestData.PendingSubscription();

        var exception = Assert.Throws<InvalidOperationException>(subscription.Renew);

        Assert.Equal("subscription.error.state.notActive", exception.Message);
    }

    [Fact]
    public void CancelShouldKeepThePaidPeriodAndStopTheRenewal()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        var paidUntil = subscription.CurrentPeriodEnd;

        subscription.Cancel();

        Assert.Equal(ESubscriptionStatus.Cancelled, subscription.Status);
        Assert.False(subscription.AutoRenew);
        Assert.False(subscription.IsCurrent);
        Assert.Equal(paidUntil, subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void CancelShouldFailWhenTheSubscriptionIsPending()
    {
        var subscription = SubscriptionTestData.PendingSubscription();

        Assert.Throws<InvalidOperationException>(subscription.Cancel);
    }

    [Fact]
    public void ApplyApprovedPaymentShouldActivateAPendingSubscription()
    {
        var subscription = SubscriptionTestData.PendingSubscription();

        subscription.ApplyApprovedPayment();

        Assert.Equal(ESubscriptionStatus.Active, subscription.Status);
    }

    [Fact]
    public void ApplyApprovedPaymentShouldRenewAnActiveSubscription()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        var currentEnd = subscription.CurrentPeriodEnd!.Value;

        subscription.ApplyApprovedPayment();

        Assert.Equal(ESubscriptionStatus.Active, subscription.Status);
        Assert.Equal(currentEnd.AddMonths(1), subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void ChangePlanShouldKeepThePeriodAndChangeThePlanAndCycle()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        var paidUntil = subscription.CurrentPeriodEnd;

        subscription.ChangePlan(new PlanId(2), EBillingCycle.Annual);

        Assert.Equal(new PlanId(2), subscription.PlanId);
        Assert.Equal(EBillingCycle.Annual, subscription.BillingCycle);
        Assert.Equal(paidUntil, subscription.CurrentPeriodEnd);
    }

    [Fact]
    public void ChangePlanShouldFailWhenTheSubscriptionIsCancelled()
    {
        var subscription = SubscriptionTestData.ActiveSubscription();
        subscription.Cancel();

        Assert.Throws<InvalidOperationException>(() => subscription.ChangePlan(new PlanId(2), EBillingCycle.Annual));
    }

    [Fact]
    public void RecordPaymentShouldAddThePaymentAndNumberTheNextInvoice()
    {
        var subscription = SubscriptionTestData.PendingSubscription();
        var first = subscription.NextInvoiceNumber;

        subscription.RecordPayment(new Payment(first, new Money(199m, "USD")));

        Assert.Single(subscription.Payments);
        Assert.EndsWith("-1", first);
        Assert.EndsWith("-2", subscription.NextInvoiceNumber);
    }
}
