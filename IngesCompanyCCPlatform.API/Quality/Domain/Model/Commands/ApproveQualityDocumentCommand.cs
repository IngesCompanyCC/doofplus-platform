using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to approve the current version of a quality document with an electronic signature.
/// </summary>
public record ApproveQualityDocumentCommand(QualityDocumentId QualityDocumentId, UserId ApproverId, ElectronicSignatureId SignatureId);
