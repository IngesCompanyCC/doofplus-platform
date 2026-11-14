using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a collaboration task.
/// </summary>
public record CreateCollaborationTaskCommand(
    string Title,
    string Description,
    int? AssignedTo,
    int? CreatedBy,
    int? RelatedDeviationId,
    DateOnly? DueDate);
