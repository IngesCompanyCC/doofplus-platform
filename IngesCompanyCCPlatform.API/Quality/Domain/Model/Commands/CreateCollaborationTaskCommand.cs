using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a collaboration task.
/// </summary>
public record CreateCollaborationTaskCommand(
    string Title,
    string RecordCode,
    string Detail,
    string Owner,
    DateTime? DueAt,
    ETaskStatus Status);
