using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to get a production order by its id.
/// </summary>
public record GetProductionOrderByIdQuery(
    ProductionOrderId ProductionOrderId);
