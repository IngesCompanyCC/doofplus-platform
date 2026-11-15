namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A analytical result, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record AnalyticalResultResource(
    int Id,
    string BatchCode,
    string SampleCode,
    string ReportCode,
    string Test,
    string Protocol,
    string Result,
    string Specification,
    string Analyst,
    string Status,
    string Approval);
