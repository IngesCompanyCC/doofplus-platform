using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;

/// <summary>
///     Represents a corrective or preventive action (CAPA) that resolves a deviation.
/// </summary>
public class CapaAction
{
    private const string DescriptionNotBlankMessageKey = "deviation.error.capaAction.description.notBlank";
    private const string ProgressRangeMessageKey = "deviation.error.capaAction.progress.range";

    /// <summary>
    ///     Initializes a new instance of the <see cref="CapaAction" /> entity. Used by Entity Framework Core.
    /// </summary>
    protected CapaAction()
    {
        Description = string.Empty;
    }

    /// <param name="type">Whether the action is corrective or preventive.</param>
    /// <param name="description">What has to be done.</param>
    /// <param name="responsibleId">The user responsible for the action.</param>
    /// <param name="dueDate">The date the action is due.</param>
    public CapaAction(ECapaType type, string description, UserId responsibleId, DateOnly dueDate)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Type = type;
        Description = description;
        ResponsibleId = responsibleId;
        DueDate = dueDate;
        Progress = 0;
        Status = ECapaStatus.Open;
    }

    public int Id { get; private set; }
    public ECapaType Type { get; private set; }
    public string Description { get; private set; }
    public UserId ResponsibleId { get; private set; }
    public DateOnly DueDate { get; private set; }

    /// <summary>
    ///     Gets the progress of the action, from 0 to 100.
    /// </summary>
    public int Progress { get; private set; }

    public ECapaStatus Status { get; private set; }

    /// <summary>
    ///     Updates the progress. At 100 the action is implemented and waits for verification.
    /// </summary>
    /// <param name="progress">The progress, from 0 to 100.</param>
    public void UpdateProgress(int progress)
    {
        if (progress is < 0 or > 100) throw new ArgumentException(ProgressRangeMessageKey);
        if (Status == ECapaStatus.Verified)
            throw new InvalidOperationException("A verified CAPA action cannot change its progress.");
        Progress = progress;
        Status = progress == 100 ? ECapaStatus.Implemented : ECapaStatus.Open;
    }

    /// <summary>
    ///     Verifies that the implemented action was effective.
    /// </summary>
    public void Verify()
    {
        if (Status != ECapaStatus.Implemented)
            throw new InvalidOperationException("Only an implemented CAPA action can be verified.");
        Status = ECapaStatus.Verified;
    }

    /// <summary>
    ///     Checks whether the action passed its due date without being implemented.
    /// </summary>
    /// <param name="today">The current date.</param>
    /// <returns><c>true</c> when the action is overdue.</returns>
    public bool IsOverdue(DateOnly today)
    {
        return Status is ECapaStatus.Open or ECapaStatus.Overdue && DueDate < today;
    }
}
