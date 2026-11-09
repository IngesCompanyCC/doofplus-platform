using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to reject a raw material lot.
/// </summary>
public record RejectMaterialCommand(MaterialApprovalId MaterialApprovalId, UserId DecidedBy, string Reason);
