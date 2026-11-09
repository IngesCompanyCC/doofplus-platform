using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to release an evaluated batch with an electronic signature.
/// </summary>
public record ReleaseBatchCommand(BatchReviewId BatchReviewId, ElectronicSignatureId SignatureId);
