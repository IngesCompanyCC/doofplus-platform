namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a audit.
/// </summary>
public record CreateAuditResource(
    string Code,
    string Title,
    string? Scope,
    string? AuditType,
    int? AuditorId,
    DateOnly? PlannedDate);
