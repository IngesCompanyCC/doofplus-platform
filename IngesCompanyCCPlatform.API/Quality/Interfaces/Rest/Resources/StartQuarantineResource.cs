namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to put a production batch in quarantine.
/// </summary>
public record StartQuarantineResource(int ProductionBatchId);
