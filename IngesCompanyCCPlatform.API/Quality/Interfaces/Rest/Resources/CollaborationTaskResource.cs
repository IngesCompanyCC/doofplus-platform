namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A collaboration task, as the API returns it.
/// </summary>
public record CollaborationTaskResource(
    int Id,
    string Title,
    string Description,
    int? AssignedTo,
    int? CreatedBy,
    int? RelatedDeviationId,
    DateOnly? DueDate,
    string Status,
    DateTime? CompletedAt);
