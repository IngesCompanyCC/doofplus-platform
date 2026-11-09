using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to attach the evidence package delivered to the auditor.
/// </summary>
public record AttachAuditEvidencePackageCommand(AuditId AuditId, string FileUrl);
