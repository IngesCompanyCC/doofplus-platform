namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a capa action.
/// </summary>
public record UpdateCapaActionResource(
    string? CapaCode,
    string? Code,
    string? Type,
    string? Title,
    string? Owner,
    DateOnly? DueAt,
    string? Status,
    string? Evidence,
    bool? EvidenceAttached);
