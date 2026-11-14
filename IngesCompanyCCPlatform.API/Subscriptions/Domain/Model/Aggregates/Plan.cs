using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;

/// <summary>
///     Represents the Plan aggregate root within the Subscriptions bounded context.
/// </summary>
/// <remarks>
///     A plan is offered by DoofPlus with its monthly and annual prices and the user and IoT device limits.
/// </remarks>
public partial class Plan : AggregateRoot
{
    private const string NameNotBlankMessageKey = "plan.error.name.notBlank";
    private const string LimitsInvalidMessageKey = "plan.error.limits.invalid";
    private const string CurrencyMismatchMessageKey = "plan.error.currency.mismatch";

    /// <summary>
    ///     Initializes a new instance of the <see cref="Plan" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected Plan()
    {
        PlanKey = null!;
        Name = string.Empty;
        Currency = string.Empty;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Plan" /> aggregate using a <see cref="CreatePlanCommand" />.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreatePlanCommand" /> containing the data of the plan.
    /// </param>
    public Plan(CreatePlanCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Name)) throw new ArgumentException(NameNotBlankMessageKey);
        if (command.UserLimit < 0 || command.IotDeviceLimit < 0) throw new ArgumentException(LimitsInvalidMessageKey);
        if (command.MonthlyPrice.Currency != command.AnnualPrice.Currency)
            throw new ArgumentException(CurrencyMismatchMessageKey);
        PlanKey = command.PlanKey;
        Name = command.Name;
        MonthlyPriceAmount = command.MonthlyPrice.Amount;
        AnnualPriceAmount = command.AnnualPrice.Amount;
        Currency = command.MonthlyPrice.Currency;
        UserLimit = command.UserLimit;
        IotDeviceLimit = command.IotDeviceLimit;
    }

    /// <summary>
    ///     Gets the unique identifier of the plan.
    /// </summary>
    public PlanId Id { get; }

    /// <summary>
    ///     Gets the unique business key of the plan, for example <c>standard-lab</c>.
    /// </summary>
    public PlanKey PlanKey { get; private set; }

    /// <summary>
    ///     Gets the display name of the plan.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the amount of the monthly cycle, in <see cref="Currency" />.
    /// </summary>
    public decimal MonthlyPriceAmount { get; private set; }

    /// <summary>
    ///     Gets the amount of the annual cycle, in <see cref="Currency" />.
    /// </summary>
    public decimal AnnualPriceAmount { get; private set; }

    /// <summary>
    ///     Gets the ISO 4217 currency shared by both prices, for example <c>USD</c>.
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    ///     Gets the price of the monthly cycle.
    /// </summary>
    public Money MonthlyPrice => new(MonthlyPriceAmount, Currency);

    /// <summary>
    ///     Gets the price of the annual cycle.
    /// </summary>
    public Money AnnualPrice => new(AnnualPriceAmount, Currency);

    /// <summary>
    ///     Gets the maximum number of users of the plan; 0 means unlimited.
    /// </summary>
    public int UserLimit { get; private set; }

    /// <summary>
    ///     Gets the maximum number of IoT devices of the plan; 0 means unlimited.
    /// </summary>
    public int IotDeviceLimit { get; private set; }

    /// <summary>
    ///     Gets the price of the plan for a billing cycle.
    /// </summary>
    /// <param name="cycle">
    ///     The billing cycle.
    /// </param>
    /// <returns>
    ///     The monthly or the annual price.
    /// </returns>
    public Money GetPrice(EBillingCycle cycle)
    {
        return cycle == EBillingCycle.Monthly ? MonthlyPrice : AnnualPrice;
    }

    /// <summary>
    ///     Checks the plan limit before registering one more IoT device (US51, scenario 2).
    /// </summary>
    /// <param name="registeredDevices">
    ///     The devices already registered by the organization.
    /// </param>
    /// <returns>
    ///     <c>true</c> when one more device fits in the plan or the plan has no limit (0).
    /// </returns>
    public bool AllowsOneMoreIotDevice(int registeredDevices)
    {
        return IotDeviceLimit == 0 || registeredDevices < IotDeviceLimit;
    }
}
