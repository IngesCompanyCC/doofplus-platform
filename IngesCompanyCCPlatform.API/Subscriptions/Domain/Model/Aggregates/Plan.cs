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

    /// <summary>
    ///     Initializes a new instance of the <see cref="Plan" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected Plan()
    {
        PlanKey = null!;
        Name = string.Empty;
        MonthlyPrice = null!;
        AnnualPrice = null!;
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
        if (command.UserLimit <= 0 || command.IotDeviceLimit < 0) throw new ArgumentException(LimitsInvalidMessageKey);
        PlanKey = command.PlanKey;
        Name = command.Name;
        MonthlyPrice = command.MonthlyPrice;
        AnnualPrice = command.AnnualPrice;
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
    ///     Gets the price of the monthly cycle.
    /// </summary>
    public Money MonthlyPrice { get; private set; }

    /// <summary>
    ///     Gets the price of the annual cycle.
    /// </summary>
    public Money AnnualPrice { get; private set; }

    /// <summary>
    ///     Gets the maximum number of users of the plan.
    /// </summary>
    public int UserLimit { get; private set; }

    /// <summary>
    ///     Gets the maximum number of IoT devices of the plan.
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
    ///     <c>true</c> when one more device fits in the plan.
    /// </returns>
    public bool AllowsOneMoreIotDevice(int registeredDevices)
    {
        return registeredDevices < IotDeviceLimit;
    }
}
