namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A deviation, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record DeviationResource(
    int Id,
    string Code,
    string Title,
    string BatchCode,
    string OrderCode,
    string Summary,
    string Classification,
    string Containment,
    int Severity,
    int Likelihood,
    int Detectability,
    string Status,
    string RootCause,
    string CapaCode,
    string ReportedBy,
    DateTime? ReportedAt,
    string Owner,
    DateTime? DueAt);
