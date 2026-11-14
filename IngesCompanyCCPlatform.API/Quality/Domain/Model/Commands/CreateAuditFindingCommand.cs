using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a audit finding.
/// </summary>
public record CreateAuditFindingCommand(
    int AuditId,
    string Title,
    string Description,
    EAuditFindingSeverity Severity,
    int? RelatedDeviationId);
