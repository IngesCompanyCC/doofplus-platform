namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A traceability gap, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record TraceabilityGapResource(
    int Id,
    string BatchCode,
    string Product,
    string MissingRecord,
    string Detail,
    string Status);
