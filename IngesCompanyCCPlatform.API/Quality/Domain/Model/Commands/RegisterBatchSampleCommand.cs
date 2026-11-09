using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register the sample taken from a batch in quarantine.
/// </summary>
public record RegisterBatchSampleCommand(BatchReviewId BatchReviewId, string SampleCode);
