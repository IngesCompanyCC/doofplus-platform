using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Commands;

/// <summary>
///     Command to create a subscription plan.
/// </summary>
/// <param name="PlanKey">
///     The unique business key of the plan.
/// </param>
/// <param name="Name">
///     The display name of the plan.
/// </param>
/// <param name="MonthlyPrice">
///     The price of the monthly cycle.
/// </param>
/// <param name="AnnualPrice">
///     The price of the annual cycle.
/// </param>
/// <param name="UserLimit">
///     The maximum number of users.
/// </param>
/// <param name="IotDeviceLimit">
///     The maximum number of IoT devices.
/// </param>
public record CreatePlanCommand(
    PlanKey PlanKey,
    string Name,
    Money MonthlyPrice,
    Money AnnualPrice,
    int UserLimit,
    int IotDeviceLimit);
