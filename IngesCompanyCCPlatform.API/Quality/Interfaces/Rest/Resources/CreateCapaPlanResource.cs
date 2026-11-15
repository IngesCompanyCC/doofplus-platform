namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a capa plan. Missing optional values take their default.
/// </summary>
public record CreateCapaPlanResource(
    string? Code,
    string? Title,
    string? SourceDeviation,
    string? Owner,
    string? Reviewer,
    string? RootCause,
    string? AcceptanceCriterion,
    DateOnly? EffectivenessCheckAt,
    string? Status);
