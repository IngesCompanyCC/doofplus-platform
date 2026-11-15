namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a deviation.
/// </summary>
public record UpdateDeviationResource(
    string? Code,
    string? Title,
    string? BatchCode,
    string? OrderCode,
    string? Summary,
    string? Classification,
    string? Containment,
    int? Severity,
    int? Likelihood,
    int? Detectability,
    string? Status,
    string? RootCause,
    string? CapaCode,
    string? ReportedBy,
    DateTime? ReportedAt,
    string? Owner,
    DateTime? DueAt);
