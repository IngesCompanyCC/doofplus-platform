namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to start the manufacturing of a planned batch.
/// </summary>
/// <param name="BatchId">The identifier of the batch.</param>
public record StartBatchCommand(int BatchId);
