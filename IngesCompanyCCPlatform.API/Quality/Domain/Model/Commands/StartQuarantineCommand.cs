using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to put a production batch in quarantine and open its review (sent by Manufacturing).
/// </summary>
public record StartQuarantineCommand(ProductionBatchId ProductionBatchId);
