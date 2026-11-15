namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A regulatory report, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record RegulatoryReportResource(
    int Id,
    string Code,
    string Type,
    string Scope,
    string Owner,
    string Status,
    string Template,
    string Format,
    DateOnly? CreatedAt);
