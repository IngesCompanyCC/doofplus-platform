using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to close an audit in progress.
/// </summary>
public record CloseAuditCommand(AuditId AuditId);
