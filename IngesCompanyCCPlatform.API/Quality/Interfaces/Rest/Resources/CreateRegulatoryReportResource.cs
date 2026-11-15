namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a regulatory report. Missing optional values take their default.
/// </summary>
public record CreateRegulatoryReportResource(
    string? Code,
    string? Type,
    string? Scope,
    string? Owner,
    string? Status,
    string? Template,
    string? Format,
    DateOnly? CreatedAt);
