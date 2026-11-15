namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a audit finding. Missing optional values take their default.
/// </summary>
public record CreateAuditFindingResource(
    string? AuditCode,
    string? Code,
    string? Title,
    string? Classification,
    string? Owner,
    DateOnly? DueAt,
    string? Status,
    string? Observation,
    string? Response,
    string? ClosureRequirement);
