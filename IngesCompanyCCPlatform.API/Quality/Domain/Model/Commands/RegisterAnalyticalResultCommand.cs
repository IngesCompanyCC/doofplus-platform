using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register the result of an analytical test of a production batch.
/// </summary>
public record RegisterAnalyticalResultCommand(ProductionBatchId ProductionBatchId, string TestName, decimal Value, SpecificationRange Specification, UserId AnalystId);
