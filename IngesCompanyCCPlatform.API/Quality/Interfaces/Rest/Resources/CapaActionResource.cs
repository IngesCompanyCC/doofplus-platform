namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A capa action, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record CapaActionResource(
    int Id,
    string CapaCode,
    string Code,
    string Type,
    string Title,
    string Owner,
    DateOnly? DueAt,
    string Status,
    string Evidence,
    bool EvidenceAttached);
