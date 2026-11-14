namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A capa action, as the API returns it.
/// </summary>
public record CapaActionResource(
    int Id,
    int CapaPlanId,
    string Title,
    string Description,
    string AssignedTo,
    DateOnly DueDate,
    string Status,
    DateTime? CompletedAt);
