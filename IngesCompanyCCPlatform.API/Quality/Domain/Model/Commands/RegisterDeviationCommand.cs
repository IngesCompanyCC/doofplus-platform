using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register a deviation found in a production batch.
/// </summary>
public record RegisterDeviationCommand(string Code, ProductionBatchId ProductionBatchId, EDeviationSeverity Severity, string Description);
