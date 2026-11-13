namespace IngesCompanyCCPlatform.API.Subscriptions.Interfaces.Rest.Resources;

/// <summary>
///     Request resource used to create a subscription plan.
/// </summary>
/// <param name="PlanKey">The unique plan key in kebab case, for example <c>standard-lab</c>.</param>
/// <param name="Name">The display name, for example <c>Standard Lab</c>.</param>
/// <param name="MonthlyPrice">The price of the monthly cycle.</param>
/// <param name="AnnualPrice">The price of the annual cycle.</param>
/// <param name="Currency">The ISO 4217 currency code, for example <c>PEN</c>.</param>
/// <param name="UserLimit">The maximum number of users.</param>
/// <param name="IotDeviceLimit">The maximum number of IoT devices.</param>
public record CreatePlanResource(
    string PlanKey,
    string Name,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    string Currency,
    int UserLimit,
    int IotDeviceLimit);
