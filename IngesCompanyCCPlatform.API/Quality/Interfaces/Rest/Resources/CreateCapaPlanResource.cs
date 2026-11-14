namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a capa plan.
/// </summary>
public record CreateCapaPlanResource(
    int DeviationId,
    string Title,
    string Description,
    DateOnly DueDate,
    string Responsible);
