namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to record the progress of a production batch.
/// </summary>
/// <param name="BatchId">The identifier of the batch.</param>
/// <param name="Progress">The progress, from 0 to 100.</param>
public record UpdateBatchProgressCommand(int BatchId, int Progress);
