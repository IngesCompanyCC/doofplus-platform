namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Response representation of a batch lifecycle event.
/// </summary>
public record BatchEventResource(
    int Id,
    int ProductionBatchId,
    string Type,
    string Description,
    long? PerformedBy,
    DateTime OccurredAt);
