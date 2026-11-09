using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register a finding or an observation during an audit.
/// </summary>
public record RegisterAuditFindingCommand(AuditId AuditId, EFindingType Type, string Description);
