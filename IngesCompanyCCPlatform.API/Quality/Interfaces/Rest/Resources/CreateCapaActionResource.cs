namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a capa action.
/// </summary>
public record CreateCapaActionResource(
    int CapaPlanId,
    string Title,
    string Description,
    string AssignedTo,
    DateOnly DueDate);
