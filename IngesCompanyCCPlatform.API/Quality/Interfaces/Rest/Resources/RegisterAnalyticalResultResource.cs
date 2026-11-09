namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to register an analytical result.
/// </summary>
public record RegisterAnalyticalResultResource(int ProductionBatchId, string TestName, decimal Value, decimal SpecMin, decimal SpecMax, string Unit, int AnalystId);
