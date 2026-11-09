using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the analytical results of a production batch.
/// </summary>
public record GetAnalyticalResultsByProductionBatchIdQuery(ProductionBatchId ProductionBatchId);
