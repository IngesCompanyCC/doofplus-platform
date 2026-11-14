using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A plan of corrective and preventive actions (CAPA) opened for a deviation.
/// </summary>
public partial class CapaPlan : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "quality.capaPlan.error.title.notBlank";
    private const string DescriptionNotBlankMessageKey = "quality.capaPlan.error.description.notBlank";
    private const string ResponsibleNotBlankMessageKey = "quality.capaPlan.error.responsible.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected CapaPlan()
    {
        Title = string.Empty;
        Description = string.Empty;
        Responsible = string.Empty;
    }

    /// <summary>
    ///     Creates the capa plan from its command.
    /// </summary>
    /// <param name="command">The data of the capa plan.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public CapaPlan(CreateCapaPlanCommand command) : this()
    {
        DeviationId = command.DeviationId;
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        if (string.IsNullOrWhiteSpace(command.Description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Description = command.Description.Trim();
        DueDate = command.DueDate;
        if (string.IsNullOrWhiteSpace(command.Responsible)) throw new ArgumentException(ResponsibleNotBlankMessageKey);
        Responsible = command.Responsible.Trim();
        Status = ECapaStatus.Open;
    }

    /// <summary>
    ///     The id of the capa plan.
    /// </summary>
    public CapaPlanId Id { get; }

    /// <summary>
    ///     The id of the deviation that the plan corrects.
    /// </summary>
    public int DeviationId { get; private set; }
    /// <summary>
    ///     The title of the plan.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What the plan has to achieve.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     When the plan has to be completed.
    /// </summary>
    public DateOnly DueDate { get; private set; }
    /// <summary>
    ///     Who is responsible for the plan.
    /// </summary>
    public string Responsible { get; private set; }
    /// <summary>
    ///     Where the plan is.
    /// </summary>
    public ECapaStatus Status { get; private set; }
    /// <summary>
    ///     When the plan was completed (UTC).
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    ///     Starts an open plan.
    /// </summary>
    public bool Start()
    {
        if (Status != ECapaStatus.Open) return false;
        Status = ECapaStatus.InProgress;
        return true;
    }

    /// <summary>
    ///     Sends a plan in progress to verification of effectiveness.
    /// </summary>
    public bool RequestVerification()
    {
        if (Status != ECapaStatus.InProgress) return false;
        Status = ECapaStatus.PendingVerification;
        return true;
    }

    /// <summary>
    ///     Completes a verified plan.
    /// </summary>
    public bool Complete()
    {
        if (Status != ECapaStatus.PendingVerification) return false;
        Status = ECapaStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Cancels a plan that is not completed.
    /// </summary>
    public bool Cancel()
    {
        if (Status == ECapaStatus.Completed) return false;
        Status = ECapaStatus.Cancelled;
        return true;
    }
}
