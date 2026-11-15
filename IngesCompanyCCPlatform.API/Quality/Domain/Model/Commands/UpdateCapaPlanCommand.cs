using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to replace the data of a capa plan.
/// </summary>
public record UpdateCapaPlanCommand(
    int CapaPlanId,
    string Code,
    string Title,
    string SourceDeviation,
    string Owner,
    string Reviewer,
    string RootCause,
    string AcceptanceCriterion,
    DateOnly? EffectivenessCheckAt,
    ECapaStatus Status);
