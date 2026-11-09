using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the Deviation aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     A deviation is classified, investigated until its root cause is found, and resolved with CAPA
///     actions. It can only be closed with a root cause and all its CAPA actions verified (US19).
/// </remarks>
public partial class Deviation : AggregateRoot
{
    private readonly List<CapaAction> _actions = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="Deviation" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected Deviation()
    {
        Code = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    ///     Initializes an open deviation.
    /// </summary>
    /// <param name="command">The <see cref="RegisterDeviationCommand" /> with the deviation data.</param>
    public Deviation(RegisterDeviationCommand command) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Code);
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Description);
        Code = command.Code;
        ProductionBatchId = command.ProductionBatchId;
        Severity = command.Severity;
        Description = command.Description;
        Status = EDeviationStatus.Open;
        DetectedAt = DateTime.UtcNow;
        if (Severity == EDeviationSeverity.Critical)
            AddDomainEvent(new CriticalDeviationRegisteredEvent(Code, ProductionBatchId));
    }

    public DeviationId Id { get; }

    /// <summary>
    ///     Gets the unique code of the deviation, for example <c>DEV-26017</c>.
    /// </summary>
    public string Code { get; private set; }

    public ProductionBatchId ProductionBatchId { get; private set; }
    public EDeviationSeverity Severity { get; private set; }
    public EDeviationStatus Status { get; private set; }
    public string Description { get; private set; }
    public RootCause? RootCause { get; private set; }
    public DateTime DetectedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public IReadOnlyCollection<CapaAction> Actions => _actions.AsReadOnly();

    /// <summary>
    ///     Changes the severity of a deviation that is not closed.
    /// </summary>
    /// <param name="severity">The new severity.</param>
    public void Classify(EDeviationSeverity severity)
    {
        EnsureNotClosed();
        Severity = severity;
    }

    /// <summary>
    ///     Starts the investigation of an open deviation.
    /// </summary>
    public void StartInvestigation()
    {
        if (Status != EDeviationStatus.Open)
            throw new InvalidOperationException("Only an open deviation can be investigated.");
        Status = EDeviationStatus.UnderInvestigation;
    }

    /// <summary>
    ///     Registers the root cause found by the investigation.
    /// </summary>
    /// <param name="rootCause">The root cause and its analysis method.</param>
    public void RegisterRootCause(RootCause rootCause)
    {
        EnsureUnderInvestigation();
        RootCause = rootCause;
    }

    /// <summary>
    ///     Adds a corrective or preventive action to the deviation.
    /// </summary>
    /// <param name="action">The CAPA action.</param>
    public void AddCapaAction(CapaAction action)
    {
        EnsureUnderInvestigation();
        _actions.Add(action);
    }

    /// <summary>
    ///     Updates the progress of one of the CAPA actions.
    /// </summary>
    /// <param name="capaActionId">The id of the CAPA action.</param>
    /// <param name="progress">The progress, from 0 to 100.</param>
    public void UpdateCapaActionProgress(int capaActionId, int progress)
    {
        EnsureNotClosed();
        FindCapaAction(capaActionId).UpdateProgress(progress);
    }

    /// <summary>
    ///     Verifies one of the implemented CAPA actions.
    /// </summary>
    /// <param name="capaActionId">The id of the CAPA action.</param>
    public void VerifyCapaAction(int capaActionId)
    {
        EnsureNotClosed();
        FindCapaAction(capaActionId).Verify();
    }

    /// <summary>
    ///     Closes the deviation. Requires a root cause and at least one CAPA action, all of them verified.
    /// </summary>
    public void Close()
    {
        EnsureUnderInvestigation();
        if (RootCause is null)
            throw new InvalidOperationException("A deviation cannot be closed without a root cause.");
        if (_actions.Count == 0 || _actions.Any(action => action.Status != ECapaStatus.Verified))
            throw new InvalidOperationException("A deviation can only be closed when all its CAPA actions are verified.");
        Status = EDeviationStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        AddDomainEvent(new DeviationClosedEvent(Code, ProductionBatchId));
    }

    private CapaAction FindCapaAction(int capaActionId)
    {
        return _actions.FirstOrDefault(action => action.Id == capaActionId)
               ?? throw new InvalidOperationException($"CAPA action {capaActionId} does not belong to the deviation.");
    }

    private void EnsureUnderInvestigation()
    {
        if (Status != EDeviationStatus.UnderInvestigation)
            throw new InvalidOperationException("The deviation must be under investigation.");
    }

    private void EnsureNotClosed()
    {
        if (Status == EDeviationStatus.Closed)
            throw new InvalidOperationException("A closed deviation cannot be changed.");
    }
}
