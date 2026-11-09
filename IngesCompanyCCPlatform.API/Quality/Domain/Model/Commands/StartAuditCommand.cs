using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to start a scheduled audit.
/// </summary>
public record StartAuditCommand(AuditId AuditId);
