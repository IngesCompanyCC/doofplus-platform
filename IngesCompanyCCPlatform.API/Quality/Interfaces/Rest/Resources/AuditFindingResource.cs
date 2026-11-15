namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit finding, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record AuditFindingResource(
    int Id,
    string AuditCode,
    string Code,
    string Title,
    string Classification,
    string Owner,
    DateOnly? DueAt,
    string Status,
    string Observation,
    string Response,
    string ClosureRequirement);
