using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A task shared between Quality and Production.
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
        Description = string.Empty;
    }

    /// <summary>
    ///     Creates the collaboration task from its command.
    /// </summary>
    /// <param name="command">The data of the collaboration task.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CollaborationTask(CreateCollaborationTaskCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        AssignedTo = command.AssignedTo;
        CreatedBy = command.CreatedBy;
        RelatedDeviationId = command.RelatedDeviationId;
        DueDate = command.DueDate;
        Status = ECollaborationTaskStatus.Pending;
    }

    /// <summary>
    ///     The id of the collaboration task.
    /// </summary>
    public CollaborationTaskId Id { get; }

    /// <summary>
    ///     The title of the task.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What has to be done.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     The id of the user who has to do it.
    /// </summary>
    public int? AssignedTo { get; private set; }
    /// <summary>
    ///     The id of the user who created it.
    /// </summary>
    public int? CreatedBy { get; private set; }
    /// <summary>
    ///     The id of the related deviation.
    /// </summary>
    public int? RelatedDeviationId { get; private set; }
    /// <summary>
    ///     When it has to be done.
    /// </summary>
    public DateOnly? DueDate { get; private set; }
    /// <summary>
    ///     Where the task is.
    /// </summary>
    public ECollaborationTaskStatus Status { get; private set; }
    /// <summary>
    ///     When the task was completed (UTC).
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    ///     Assigns an open task to a user.
    /// </summary>
    public bool AssignTo(int userId)
    {
        if (Status is ECollaborationTaskStatus.Completed or ECollaborationTaskStatus.Cancelled) return false;
        AssignedTo = userId;
        return true;
    }

    /// <summary>
    ///     Starts a pending task.
    /// </summary>
    public bool Start()
    {
        if (Status != ECollaborationTaskStatus.Pending) return false;
        Status = ECollaborationTaskStatus.InProgress;
        return true;
    }

    /// <summary>
    ///     Completes a task in progress.
    /// </summary>
    public bool Complete()
    {
        if (Status != ECollaborationTaskStatus.InProgress) return false;
        Status = ECollaborationTaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Cancels a task that is not completed.
    /// </summary>
    public bool Cancel()
    {
        if (Status == ECollaborationTaskStatus.Completed) return false;
        Status = ECollaborationTaskStatus.Cancelled;
        return true;
    }
}
