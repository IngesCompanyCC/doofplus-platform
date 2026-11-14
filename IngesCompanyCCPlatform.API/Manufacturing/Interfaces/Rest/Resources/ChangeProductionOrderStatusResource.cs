namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     The new status of a production order: InProgress, OnHold or Completed.
/// </summary>
public record ChangeProductionOrderStatusResource(
    string Status);
