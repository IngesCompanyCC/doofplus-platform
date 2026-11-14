using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a capa action.
/// </summary>
public record CreateCapaActionCommand(
    int CapaPlanId,
    string Title,
    string Description,
    string AssignedTo,
    DateOnly DueDate);
