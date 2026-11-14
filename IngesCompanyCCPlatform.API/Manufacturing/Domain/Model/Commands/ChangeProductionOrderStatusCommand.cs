using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to start, hold, resume or complete a production order.
/// </summary>
public record ChangeProductionOrderStatusCommand(
    ProductionOrderId ProductionOrderId,
    EProductionOrderStatus Status);
