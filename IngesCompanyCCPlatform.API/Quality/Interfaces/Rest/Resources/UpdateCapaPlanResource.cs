namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a capa plan.
/// </summary>
public record UpdateCapaPlanResource(
    string? Code,
    string? Title,
    string? SourceDeviation,
    string? Owner,
    string? Reviewer,
    string? RootCause,
    string? AcceptanceCriterion,
    DateOnly? EffectivenessCheckAt,
    string? Status);
