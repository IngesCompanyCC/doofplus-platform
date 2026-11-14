namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A capa plan, as the API returns it.
/// </summary>
public record CapaPlanResource(
    int Id,
    int DeviationId,
    string Title,
    string Description,
    DateOnly DueDate,
    string Responsible,
    string Status,
    DateTime? CompletedAt,
    DateTimeOffset? CreatedAt);
