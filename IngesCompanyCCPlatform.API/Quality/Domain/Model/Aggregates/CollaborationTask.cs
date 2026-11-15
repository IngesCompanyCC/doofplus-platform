using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Collaboration task of the Quality &amp; Compliance bounded context, as in <c>class-06-quality</c>.
/// </summary>
public partial class CollaborationTask : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "quality.collaborationTask.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected CollaborationTask()
    {
        Title = string.Empty;
        RecordCode = string.Empty;
        Detail = string.Empty;
        Owner = string.Empty;
    }

    /// <summary>
    ///     Creates the collaboration task from its command.
    /// </summary>
    /// <param name="command">The data of the collaboration task.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CollaborationTask(CreateCollaborationTaskCommand command) : this() => Apply(command.Title, command.RecordCode, command.Detail, command.Owner, command.DueAt, command.Status);

    /// <summary>
    ///     The id of the collaboration task.
    /// </summary>
    public CollaborationTaskId Id { get; }

    /// <summary>
    ///     The title.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     The record code.
    /// </summary>
    public string RecordCode { get; private set; }

    /// <summary>
    ///     The detail.
    /// </summary>
    public string Detail { get; private set; }

    /// <summary>
    ///     The owner.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    ///     The due at.
    /// </summary>
    public DateTime? DueAt { get; private set; }

    /// <summary>
    ///     The status.
    /// </summary>
    public ETaskStatus Status { get; private set; }

    private void Apply(string title, string recordCode, string detail, string owner, DateTime? dueAt, ETaskStatus status)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = title?.Trim() ?? string.Empty;
        RecordCode = recordCode?.Trim() ?? string.Empty;
        Detail = detail?.Trim() ?? string.Empty;
        Owner = owner?.Trim() ?? string.Empty;
        DueAt = dueAt;
        Status = status;
    }
}
