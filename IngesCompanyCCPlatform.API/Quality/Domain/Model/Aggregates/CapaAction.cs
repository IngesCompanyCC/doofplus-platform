using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     An action of a CAPA plan.
/// </summary>
public partial class CapaAction : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "quality.capaAction.error.title.notBlank";
    private const string DescriptionNotBlankMessageKey = "quality.capaAction.error.description.notBlank";
    private const string AssignedToNotBlankMessageKey = "quality.capaAction.error.assignedTo.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected CapaAction()
    {
        Title = string.Empty;
        Description = string.Empty;
        AssignedTo = string.Empty;
    }

    /// <summary>
    ///     Creates the capa action from its command.
    /// </summary>
    /// <param name="command">The data of the capa action.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CapaAction(CreateCapaActionCommand command) : this()
    {
        CapaPlanId = command.CapaPlanId;
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        if (string.IsNullOrWhiteSpace(command.Description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Description = command.Description.Trim();
        if (string.IsNullOrWhiteSpace(command.AssignedTo)) throw new ArgumentException(AssignedToNotBlankMessageKey);
        AssignedTo = command.AssignedTo.Trim();
        DueDate = command.DueDate;
        Status = ECapaStatus.Open;
    }

    /// <summary>
    ///     The id of the capa action.
    /// </summary>
    public CapaActionId Id { get; }

    /// <summary>
    ///     The id of the CAPA plan of the action.
    /// </summary>
    public int CapaPlanId { get; private set; }
    /// <summary>
    ///     The title of the action.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What has to be done.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     Who has to do it.
    /// </summary>
    public string AssignedTo { get; private set; }
    /// <summary>
    ///     When it has to be done.
    /// </summary>
    public DateOnly DueDate { get; private set; }
    /// <summary>
    ///     Where the action is.
    /// </summary>
    public ECapaStatus Status { get; private set; }
    /// <summary>
    ///     When the action was completed (UTC).
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    ///     Starts an open action.
    /// </summary>
    public bool Start()
    {
        if (Status != ECapaStatus.Open) return false;
        Status = ECapaStatus.InProgress;
        return true;
    }

    /// <summary>
    ///     Completes an action in progress.
    /// </summary>
    public bool Complete()
    {
        if (Status != ECapaStatus.InProgress) return false;
        Status = ECapaStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        return true;
    }
}
