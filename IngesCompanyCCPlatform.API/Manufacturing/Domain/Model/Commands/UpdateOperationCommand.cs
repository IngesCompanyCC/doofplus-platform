using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to record the progress of an operation of a production order.
/// </summary>
public record UpdateOperationCommand(
    ProductionOrderId ProductionOrderId,
    int Sequence,
    int Progress,
    EOperationStatus Status,
    string Reference);
