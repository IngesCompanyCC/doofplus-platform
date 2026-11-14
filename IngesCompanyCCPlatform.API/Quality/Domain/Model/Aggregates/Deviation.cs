using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A deviation from a procedure or a specification, found during production or control.
/// </summary>
public partial class Deviation : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.deviation.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.deviation.error.title.notBlank";
    private const string DescriptionNotBlankMessageKey = "quality.deviation.error.description.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Deviation()
    {
        Code = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    ///     Creates the deviation from its command.
    /// </summary>
    /// <param name="command">The data of the deviation.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public Deviation(CreateDeviationCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = command.Code.Trim();
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        if (string.IsNullOrWhiteSpace(command.Description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Description = command.Description.Trim();
        Severity = command.Severity;
        BatchId = command.BatchId;
        AssignedTo = command.AssignedTo;
        Status = EDeviationStatus.Open;
        DetectedAt = DateTime.UtcNow;
    }

    /// <summary>
    ///     The id of the deviation.
    /// </summary>
    public DeviationId Id { get; }

    /// <summary>
    ///     The business code, for example DEV-26017.
    /// </summary>
    public string Code { get; private set; }
    /// <summary>
    ///     A short title of the deviation.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What happened.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     How serious the deviation is.
    /// </summary>
    public EDeviationSeverity Severity { get; private set; }
    /// <summary>
    ///     The id of the affected production batch.
    /// </summary>
    public int BatchId { get; private set; }
    /// <summary>
    ///     The id of the user who investigates it.
    /// </summary>
    public int? AssignedTo { get; private set; }
    /// <summary>
    ///     Where the deviation is in its investigation.
    /// </summary>
    public EDeviationStatus Status { get; private set; }
    /// <summary>
    ///     When the deviation was registered (UTC).
    /// </summary>
    public DateTime DetectedAt { get; private set; }
    /// <summary>
    ///     When the deviation was closed (UTC).
    /// </summary>
    public DateTime? ClosedAt { get; private set; }

    /// <summary>
    ///     Starts the investigation of an open deviation.
    /// </summary>
    public bool StartInvestigation()
    {
        if (Status != EDeviationStatus.Open) return false;
        Status = EDeviationStatus.UnderInvestigation;
        return true;
    }

    /// <summary>
    ///     Marks that the deviation under investigation needs a CAPA plan.
    /// </summary>
    public bool RequireCapa()
    {
        if (Status != EDeviationStatus.UnderInvestigation) return false;
        Status = EDeviationStatus.CapaRequired;
        return true;
    }

    /// <summary>
    ///     Records the disposition of a deviation under investigation or waiting for a CAPA plan.
    /// </summary>
    public bool Disposition()
    {
        if (Status is not (EDeviationStatus.UnderInvestigation or EDeviationStatus.CapaRequired)) return false;
        Status = EDeviationStatus.Dispositioned;
        return true;
    }

    /// <summary>
    ///     Closes a dispositioned deviation.
    /// </summary>
    public bool Close()
    {
        if (Status != EDeviationStatus.Dispositioned) return false;
        Status = EDeviationStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        return true;
    }
}
