namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a deviation.
/// </summary>
public record CreateDeviationResource(
    string Code,
    string Title,
    string Description,
    string Severity,
    int BatchId,
    int? AssignedTo);
