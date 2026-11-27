namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Body of the rejection of a production order.
/// </summary>
/// <param name="Reason">Why the order is rejected.</param>
public record RejectProductionOrderResource(
    string? Reason);
