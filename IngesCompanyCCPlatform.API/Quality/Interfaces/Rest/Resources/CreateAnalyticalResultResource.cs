namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a analytical result. Missing optional values take their default.
/// </summary>
public record CreateAnalyticalResultResource(
    string? BatchCode,
    string? SampleCode,
    string? ReportCode,
    string? Test,
    string? Protocol,
    string? Result,
    string? Specification,
    string? Analyst,
    string? Status,
    string? Approval);
