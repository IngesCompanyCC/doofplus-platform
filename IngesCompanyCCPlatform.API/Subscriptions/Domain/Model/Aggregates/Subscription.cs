using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;

/// <summary>
///     Represents the Subscription aggregate root within the Subscriptions bounded context.
/// </summary>
/// <remarks>
///     A subscription links an organization to a plan. It starts as pending, becomes active when the
///     payment is authorized and keeps the access until the end of the paid period when cancelled.
/// </remarks>
public partial class Subscription : AggregateRoot
{
    private const string NotPendingMessageKey = "subscription.error.state.notPending";
    private const string NotActiveMessageKey = "subscription.error.state.notActive";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Subscription" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected Subscription()
    {
        PaymentCard = null!;
        BillingEmail = null!;
    }

    /// <summary>
    ///     Initializes a new pending subscription using a <see cref="CreateSubscriptionCommand" /> (US51).
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateSubscriptionCommand" /> containing the organization, the plan and the payment data.
    /// </param>
    public Subscription(CreateSubscriptionCommand command) : this()
    {
        OrganizationId = command.OrganizationId;
        PlanId = command.PlanId;
        BillingCycle = command.BillingCycle;
        PaymentCard = command.PaymentCard;
        BillingEmail = command.BillingEmail;
        Status = ESubscriptionStatus.Pending;
        AutoRenew = true;
    }

    /// <summary>
    ///     Gets the unique identifier of the subscription.
    /// </summary>
    public SubscriptionId Id { get; }

    /// <summary>
    ///     Gets the organization that owns the subscription.
    /// </summary>
    public OrganizationId OrganizationId { get; private set; }

    /// <summary>
    ///     Gets the identifier of the subscribed plan.
    /// </summary>
    public PlanId PlanId { get; private set; }

    /// <summary>
    ///     Gets the billing cycle, monthly or annual.
    /// </summary>
    public EBillingCycle BillingCycle { get; private set; }

    /// <summary>
    ///     Gets the current status of the subscription.
    /// </summary>
    public ESubscriptionStatus Status { get; private set; }

    /// <summary>
    ///     Gets whether the subscription renews at the end of the period.
    /// </summary>
    public bool AutoRenew { get; private set; }

    /// <summary>
    ///     Gets the card used to pay.
    /// </summary>
    public PaymentCard PaymentCard { get; private set; }

    /// <summary>
    ///     Gets the email that receives the invoices.
    /// </summary>
    public BillingEmail BillingEmail { get; private set; }

    /// <summary>
    ///     Gets the first day of the paid period, or <c>null</c> while the subscription is pending.
    /// </summary>
    public DateOnly? CurrentPeriodStart { get; private set; }

    /// <summary>
    ///     Gets the last day of the paid period, or <c>null</c> while the subscription is pending.
    /// </summary>
    public DateOnly? CurrentPeriodEnd { get; private set; }

    /// <summary>
    ///     Gets whether the subscription is pending or active.
    /// </summary>
    public bool IsCurrent => Status is ESubscriptionStatus.Pending or ESubscriptionStatus.Active;

    /// <summary>
    ///     Activates the subscription once the payment is authorized (US52).
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     When the subscription is not pending.
    /// </exception>
    public void Activate()
    {
        if (Status != ESubscriptionStatus.Pending) throw new InvalidOperationException(NotPendingMessageKey);
        Status = ESubscriptionStatus.Active;
        CurrentPeriodStart = DateOnly.FromDateTime(DateTime.UtcNow);
        CurrentPeriodEnd = NextPeriodEnd(CurrentPeriodStart.Value);
    }

    /// <summary>
    ///     Extends the subscription one more billing period (US53).
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     When the subscription is not active.
    /// </exception>
    public void Renew()
    {
        if (Status != ESubscriptionStatus.Active || CurrentPeriodEnd is null)
            throw new InvalidOperationException(NotActiveMessageKey);
        CurrentPeriodStart = CurrentPeriodEnd;
        CurrentPeriodEnd = NextPeriodEnd(CurrentPeriodStart.Value);
    }

    /// <summary>
    ///     Cancels the subscription; the access stays until the end of the paid period (US53).
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     When the subscription is not active.
    /// </exception>
    public void Cancel()
    {
        if (Status != ESubscriptionStatus.Active) throw new InvalidOperationException(NotActiveMessageKey);
        Status = ESubscriptionStatus.Cancelled;
        AutoRenew = false;
    }

    /// <summary>
    ///     Computes the end of the billing period that starts on the given date.
    /// </summary>
    /// <param name="from">
    ///     The start of the period.
    /// </param>
    /// <returns>
    ///     One month or one year after <paramref name="from" />, depending on the billing cycle.
    /// </returns>
    private DateOnly NextPeriodEnd(DateOnly from)
    {
        return BillingCycle == EBillingCycle.Monthly ? from.AddMonths(1) : from.AddYears(1);
    }
}
