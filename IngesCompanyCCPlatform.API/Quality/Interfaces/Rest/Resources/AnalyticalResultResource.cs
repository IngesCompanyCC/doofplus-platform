namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing an analytical result.
/// </summary>
public record AnalyticalResultResource(int Id, int ProductionBatchId, string TestName, decimal Value, decimal SpecMin, decimal SpecMax, string Unit, bool OutOfSpecification, int AnalystId);
