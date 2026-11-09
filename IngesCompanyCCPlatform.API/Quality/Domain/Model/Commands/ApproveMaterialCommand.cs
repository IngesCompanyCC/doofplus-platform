using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to approve a sampled raw material lot with an electronic signature.
/// </summary>
public record ApproveMaterialCommand(MaterialApprovalId MaterialApprovalId, UserId DecidedBy, ElectronicSignatureId SignatureId);
