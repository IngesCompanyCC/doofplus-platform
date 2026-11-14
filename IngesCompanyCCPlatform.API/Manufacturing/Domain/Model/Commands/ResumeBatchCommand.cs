namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to resume a batch that is on hold.
/// </summary>
/// <param name="BatchId">The identifier of the batch.</param>
public record ResumeBatchCommand(int BatchId);
