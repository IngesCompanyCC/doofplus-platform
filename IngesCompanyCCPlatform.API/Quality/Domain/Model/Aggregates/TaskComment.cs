using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Task comment of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class TaskComment : AggregateRoot
{
    private const string RecordCodeNotBlankMessageKey = "quality.taskComment.error.recordCode.notBlank";
    private const string TextNotBlankMessageKey = "quality.taskComment.error.text.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected TaskComment()
    {
        RecordCode = string.Empty;
        Author = string.Empty;
        Initials = string.Empty;
        Text = string.Empty;
    }

    /// <summary>
    ///     Creates the task comment from its command.
    /// </summary>
    /// <param name="command">The data of the task comment.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public TaskComment(CreateTaskCommentCommand command) : this() => Apply(command.RecordCode, command.Author, command.Initials, command.PostedAt, command.Text);

    /// <summary>
    ///     The id of the task comment.
    /// </summary>
    public TaskCommentId Id { get; }

    /// <summary>
    ///     The record code.
    /// </summary>
    public string RecordCode { get; private set; }

    /// <summary>
    ///     The author.
    /// </summary>
    public string Author { get; private set; }

    /// <summary>
    ///     The initials.
    /// </summary>
    public string Initials { get; private set; }

    /// <summary>
    ///     The posted at.
    /// </summary>
    public DateTime? PostedAt { get; private set; }

    /// <summary>
    ///     The text.
    /// </summary>
    public string Text { get; private set; }

    private void Apply(string recordCode, string author, string initials, DateTime? postedAt, string text)
    {
        if (string.IsNullOrWhiteSpace(recordCode)) throw new ArgumentException(RecordCodeNotBlankMessageKey);
        RecordCode = recordCode?.Trim() ?? string.Empty;
        Author = author?.Trim() ?? string.Empty;
        Initials = initials?.Trim() ?? string.Empty;
        PostedAt = postedAt ?? DateTime.UtcNow;
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException(TextNotBlankMessageKey);
        Text = text?.Trim() ?? string.Empty;
    }
}
