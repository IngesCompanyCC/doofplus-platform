using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;

/// <summary>
///     An operation of a production order, for example Dispensing, Granulation or Coating.
/// </summary>
public class Operation
{
    private const string ProgressInvalidMessageKey = "manufacturing.order.error.progress.invalid";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Operation()
    {
        Name = string.Empty;
        Equipment = string.Empty;
        Reference = string.Empty;
    }

    /// <summary>
    ///     Creates an operation, not started.
    /// </summary>
    public Operation(int sequence, string name, string equipment) : this()
    {
        Sequence = sequence;
        Name = name.Trim();
        Equipment = equipment?.Trim() ?? string.Empty;
        Status = EOperationStatus.NotStarted;
    }

    /// <summary>The id of the operation.</summary>
    public int Id { get; private set; }

    /// <summary>The id of its production order.</summary>
    public ProductionOrderId ProductionOrderId { get; private set; }

    /// <summary>The position of the operation in the order, starting at 1.</summary>
    public int Sequence { get; private set; }

    /// <summary>The name, for example Coating.</summary>
    public string Name { get; private set; }

    /// <summary>The equipment or station where it is done.</summary>
    public string Equipment { get; private set; }

    /// <summary>The progress, from 0 to 100.</summary>
    public int Progress { get; private set; }

    /// <summary>Where the operation is.</summary>
    public EOperationStatus Status { get; private set; }

    /// <summary>A related record, for example the deviation that stopped it.</summary>
    public string Reference { get; private set; }

    /// <summary>
    ///     Records the progress of the operation.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the progress is not between 0 and 100.</exception>
    public void Update(int progress, EOperationStatus status, string reference)
    {
        if (progress is < 0 or > 100) throw new ArgumentException(ProgressInvalidMessageKey);
        Progress = progress;
        Status = status;
        Reference = reference?.Trim() ?? string.Empty;
    }
}
