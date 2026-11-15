namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record AuditResource(
    int Id,
    string Code,
    string Title,
    string Period,
    int AreasReviewed,
    int AreasTotal,
    string Status);
