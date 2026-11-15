using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a batch release.
/// </summary>
public record CreateBatchReleaseCommand(
    int BatchId,
    string BatchCode,
    string RequestedBy);
