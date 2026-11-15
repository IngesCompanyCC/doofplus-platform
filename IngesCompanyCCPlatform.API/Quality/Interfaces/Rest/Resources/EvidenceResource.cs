namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A evidence, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record EvidenceResource(
    int Id,
    string RecordCode,
    string FileName,
    string Detail,
    string Status);
