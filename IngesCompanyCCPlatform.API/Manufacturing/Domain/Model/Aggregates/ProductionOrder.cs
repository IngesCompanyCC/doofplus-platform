using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     A production order: how much of a product to make, with which approved formula, on which line and with which
///     operations.
/// </summary>
public partial class ProductionOrder : AggregateRoot
{
    private const string QuantityInvalidMessageKey = "manufacturing.order.error.plannedQuantity.invalid";
    private const string OperationsEmptyMessageKey = "manufacturing.order.error.operations.empty";
    private const string OperationNameNotBlankMessageKey = "manufacturing.order.error.operationName.notBlank";
    private const string SequenceDuplicateMessageKey = "manufacturing.order.error.sequence.duplicate";
    private const string PlannedDatesInvalidMessageKey = "manufacturing.order.error.plannedDates.invalid";
    private const string TransitionInvalidMessageKey = "manufacturing.order.error.transition.invalid";

    private readonly List<Operation> _operations = [];

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected ProductionOrder()
    {
        Code = null!;
        ProductCode = null!;
        BatchCode = string.Empty;
        Line = string.Empty;
    }

    /// <summary>
    ///     Plans a production order for a product with its approved formula. It starts as Planned.
    /// </summary>
    /// <param name="command">The data of the order.</param>
    /// <param name="productCode">The product of the approved formula.</param>
    /// <exception cref="ArgumentException">Thrown when the quantity, the operations or the planned dates are not valid.</exception>
    public ProductionOrder(CreateProductionOrderCommand command, ProductCode productCode) : this()
    {
        if (command.PlannedQuantity <= 0) throw new ArgumentException(QuantityInvalidMessageKey);
        if (command.Operations is null || command.Operations.Count == 0)
            throw new ArgumentException(OperationsEmptyMessageKey);
        if (command.Operations.Select(o => o.Sequence).Distinct().Count() != command.Operations.Count)
            throw new ArgumentException(SequenceDuplicateMessageKey);
        if (command.PlannedStart is not null && command.PlannedEnd is not null && command.PlannedEnd < command.PlannedStart)
            throw new ArgumentException(PlannedDatesInvalidMessageKey);

        Code = command.Code;
        ProductCode = productCode;
        MasterFormulaId = command.MasterFormulaId;
        BatchCode = command.BatchCode?.Trim() ?? string.Empty;
        PlannedQuantity = command.PlannedQuantity;
        Line = command.Line?.Trim() ?? string.Empty;
        Status = EProductionOrderStatus.Planned;
        PlannedStart = command.PlannedStart?.ToUniversalTime();
        PlannedEnd = command.PlannedEnd?.ToUniversalTime();
        foreach (var operation in command.Operations.OrderBy(o => o.Sequence))
        {
            if (string.IsNullOrWhiteSpace(operation.Name)) throw new ArgumentException(OperationNameNotBlankMessageKey);
            _operations.Add(new Operation(operation.Sequence, operation.Name, operation.Equipment));
        }
    }

    /// <summary>The id of the order.</summary>
    public ProductionOrderId Id { get; }

    /// <summary>The business code, for example PO-26041.</summary>
    public ProductionOrderCode Code { get; private set; }

    /// <summary>The code of the product.</summary>
    public ProductCode ProductCode { get; private set; }

    /// <summary>The approved master formula it follows.</summary>
    public MasterFormulaId MasterFormulaId { get; private set; }

    /// <summary>The code of the production batch it produces, or empty.</summary>
    public string BatchCode { get; private set; }

    /// <summary>How many units to make.</summary>
    public decimal PlannedQuantity { get; private set; }

    /// <summary>The production line, for example Line 02.</summary>
    public string Line { get; private set; }

    /// <summary>Where the order is.</summary>
    public EProductionOrderStatus Status { get; private set; }

    /// <summary>When it is planned to start (UTC).</summary>
    public DateTime? PlannedStart { get; private set; }

    /// <summary>When it is planned to end (UTC).</summary>
    public DateTime? PlannedEnd { get; private set; }

    /// <summary>The operations, in order.</summary>
    public IReadOnlyCollection<Operation> Operations => _operations.OrderBy(o => o.Sequence).ToList().AsReadOnly();

    /// <summary>
    ///     Changes the status of the order: Planned → InProgress; InProgress ↔ OnHold; InProgress → Completed.
    /// </summary>
    /// <param name="status">The new status.</param>
    /// <exception cref="InvalidOperationException">Thrown when the change is not allowed.</exception>
    public void ChangeStatus(EProductionOrderStatus status)
    {
        var allowed = (Status, status) switch
        {
            (EProductionOrderStatus.Planned, EProductionOrderStatus.InProgress) => true,
            (EProductionOrderStatus.InProgress, EProductionOrderStatus.OnHold) => true,
            (EProductionOrderStatus.OnHold, EProductionOrderStatus.InProgress) => true,
            (EProductionOrderStatus.InProgress, EProductionOrderStatus.Completed) => true,
            _ => false
        };
        if (!allowed) throw new InvalidOperationException(TransitionInvalidMessageKey);
        Status = status;
    }

    /// <summary>
    ///     Finds an operation by its sequence.
    /// </summary>
    /// <param name="sequence">The position of the operation.</param>
    /// <returns>The operation, or null.</returns>
    public Operation? FindOperation(int sequence)
    {
        return _operations.FirstOrDefault(o => o.Sequence == sequence);
    }
}
