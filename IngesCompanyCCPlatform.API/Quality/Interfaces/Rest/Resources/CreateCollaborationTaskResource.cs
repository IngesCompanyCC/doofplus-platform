namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a collaboration task.
/// </summary>
public record CreateCollaborationTaskResource(
    string Title,
    string? Description,
    int? AssignedTo,
    int? CreatedBy,
    int? RelatedDeviationId,
    DateOnly? DueDate);
