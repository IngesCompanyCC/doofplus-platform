using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root representing a pharmaceutical production batch.
/// </summary>
public class ProductionBatch : AggregateRoot, IAuditableEntity
{
    private const string ProgressInvalidMessageKey = "manufacturing.batch.error.progress.invalid";

    private readonly List<MaterialConsumption> _consumptions = [];
    private readonly List<Incident> _incidents = [];
    private readonly List<BatchEvent> _events = [];

    public int Id { get; private set; }
    public BatchNumber BatchNumber { get; private set; } = null!;
    public int ProductionOrderId { get; private set; }
    public BatchStatus Status { get; private set; }
    public Quantity PlannedQuantity { get; private set; } = null!;
    public string? Line { get; private set; }
    public string? Owner { get; private set; }
    public int Progress { get; private set; }
    public Quantity? ManufacturedQuantity { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public IReadOnlyCollection<MaterialConsumption> Consumptions => _consumptions.AsReadOnly();
    public IReadOnlyCollection<Incident> Incidents => _incidents.AsReadOnly();
    public IReadOnlyCollection<BatchEvent> Events => _events.AsReadOnly();

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // Required by EF Core
    protected ProductionBatch()
    {
    }

    public ProductionBatch(CreateBatchCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        BatchNumber = command.BatchNumber;
        ProductionOrderId = command.ProductionOrderId;
        PlannedQuantity = command.PlannedQuantity;
        Line = string.IsNullOrWhiteSpace(command.Line) ? null : command.Line.Trim();
        Owner = string.IsNullOrWhiteSpace(command.Owner) ? null : command.Owner.Trim();
        Progress = 0;
        Status = BatchStatus.Planned;
        RecordEvent("BATCH_CREATED", "Production batch created in PLANNED status");
    }

    public void Start()
    {
        if (Status != BatchStatus.Planned)
            throw new InvalidOperationException($"Cannot start batch in status {Status}. Only PLANNED batches can be started.");

        Status = BatchStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        RecordEvent("BATCH_STARTED", "Batch manufacturing execution started");
    }

    public void Close(CloseBatchCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (Status == BatchStatus.Closed)
            throw new InvalidOperationException("Batch is already closed.");

        ManufacturedQuantity = command.ManufacturedQuantity;
        Status = BatchStatus.Closed;
        Progress = 100;
        FinishedAt = DateTime.UtcNow;
        RecordEvent("BATCH_CLOSED", $"Batch closed with manufactured quantity: {ManufacturedQuantity}");
    }

    public Incident RegisterIncident(RegisterIncidentCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        var incident = new Incident(command.Description, command.Severity);
        _incidents.Add(incident);

        if (command.Severity == IncidentSeverity.Critical)
        {
            Status = BatchStatus.OnHold;
        }

        var tone = command.Severity switch
        {
            IncidentSeverity.Critical => EventTone.Danger,
            IncidentSeverity.Major => EventTone.Warning,
            _ => EventTone.Normal
        };
        RecordEvent("INCIDENT_REGISTERED", $"Incident registered ({command.Severity}): {command.Description}", tone);
        return incident;
    }

    public void ConsumeMaterial(int rawMaterialLotId, Quantity quantity)
    {
        ArgumentNullException.ThrowIfNull(quantity);
        var consumption = new MaterialConsumption(rawMaterialLotId, quantity);
        _consumptions.Add(consumption);
        RecordEvent("MATERIAL_CONSUMED", $"Raw material lot {rawMaterialLotId} consumed ({quantity})");
    }

    /// <summary>
    ///     Records the progress of the batch shown in the Web App.
    /// </summary>
    /// <param name="progress">The progress, from 0 to 100.</param>
    /// <exception cref="ArgumentException">When the progress is outside 0 to 100.</exception>
    public void UpdateProgress(int progress)
    {
        if (progress is < 0 or > 100) throw new ArgumentException(ProgressInvalidMessageKey);
        Progress = progress;
    }

    private void RecordEvent(string type, string description, EventTone tone = EventTone.Normal, long? performedBy = null)
    {
        _events.Add(new BatchEvent(type, description, tone, performedBy));
    }
}
