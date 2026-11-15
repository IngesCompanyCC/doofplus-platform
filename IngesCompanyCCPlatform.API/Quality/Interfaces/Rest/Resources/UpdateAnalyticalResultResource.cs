namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a analytical result.
/// </summary>
public record UpdateAnalyticalResultResource(
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
