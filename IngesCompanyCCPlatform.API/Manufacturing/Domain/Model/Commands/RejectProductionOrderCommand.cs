using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to reject a planned production order with its reason (US57).
/// </summary>
/// <param name="ProductionOrderId">The order to reject.</param>
/// <param name="Reason">Why the order is rejected.</param>
public record RejectProductionOrderCommand(
    ProductionOrderId ProductionOrderId,
    string Reason);
