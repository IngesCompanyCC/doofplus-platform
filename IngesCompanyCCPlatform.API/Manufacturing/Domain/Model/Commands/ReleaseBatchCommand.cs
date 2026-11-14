namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to release a batch after the QA electronic signature.
/// </summary>
/// <param name="BatchId">The identifier of the batch.</param>
/// <param name="ReleasedBy">Who signs the release.</param>
public record ReleaseBatchCommand(int BatchId, string ReleasedBy);
