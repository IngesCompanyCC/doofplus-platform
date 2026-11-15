using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a task comment.
/// </summary>
public record CreateTaskCommentCommand(
    string RecordCode,
    string Author,
    string Initials,
    DateTime? PostedAt,
    string Text);
