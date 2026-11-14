using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a capa plan.
/// </summary>
public record CreateCapaPlanCommand(
    int DeviationId,
    string Title,
    string Description,
    DateOnly DueDate,
    string Responsible);
