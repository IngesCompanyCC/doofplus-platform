using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A comment on a collaboration task.
/// </summary>
public partial class TaskComment : AggregateRoot
{
    private const string ContentNotBlankMessageKey = "quality.taskComment.error.content.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected TaskComment()
    {
        Content = string.Empty;
    }

    /// <summary>
    ///     Creates the task comment from its command.
    /// </summary>
    /// <param name="command">The data of the task comment.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public TaskComment(CreateTaskCommentCommand command) : this()
    {
        TaskId = command.TaskId;
        AuthorId = command.AuthorId;
        if (string.IsNullOrWhiteSpace(command.Content)) throw new ArgumentException(ContentNotBlankMessageKey);
        Content = command.Content.Trim();
    }

    /// <summary>
    ///     The id of the task comment.
    /// </summary>
    public TaskCommentId Id { get; }

    /// <summary>
    ///     The id of the collaboration task.
    /// </summary>
    public int TaskId { get; private set; }
    /// <summary>
    ///     The id of the user who wrote it.
    /// </summary>
    public int AuthorId { get; private set; }
    /// <summary>
    ///     The text of the comment.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    ///     Changes the text of the comment.
    /// </summary>
    public void UpdateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException(ContentNotBlankMessageKey);
        Content = content.Trim();
    }
}
