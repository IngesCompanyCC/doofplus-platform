namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A capa plan, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record CapaPlanResource(
    int Id,
    string Code,
    string Title,
    string SourceDeviation,
    string Owner,
    string Reviewer,
    string RootCause,
    string AcceptanceCriterion,
    DateOnly? EffectivenessCheckAt,
    string Status);
