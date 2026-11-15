namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a capa action. Missing optional values take their default.
/// </summary>
public record CreateCapaActionResource(
    string? CapaCode,
    string? Code,
    string? Type,
    string? Title,
    string? Owner,
    DateOnly? DueAt,
    string? Status,
    string? Evidence,
    bool? EvidenceAttached);
