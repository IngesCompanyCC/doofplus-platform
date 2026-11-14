namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a subscription plan.
/// </summary>
/// <param name="Id">The plan identifier.</param>
/// <param name="PlanKey">The plan key, for example <c>standard-lab</c>.</param>
/// <param name="Name">The display name.</param>
/// <param name="MonthlyPrice">The price of the monthly cycle.</param>
/// <param name="AnnualPrice">The price of the annual cycle.</param>
/// <param name="Currency">The ISO 4217 currency code.</param>
/// <param name="UserLimit">The maximum number of users.</param>
/// <param name="IotDeviceLimit">The maximum number of IoT devices.</param>
public record PlanResource(
    int Id,
    string PlanKey,
    string Name,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    string Currency,
    int UserLimit,
    int IotDeviceLimit);
