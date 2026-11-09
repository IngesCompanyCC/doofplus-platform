using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the deviations of a production batch.
/// </summary>
public record GetDeviationsByProductionBatchIdQuery(ProductionBatchId ProductionBatchId);
