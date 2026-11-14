namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit, as the API returns it.
/// </summary>
public record AuditResource(
    int Id,
    string Code,
    string Title,
    string Scope,
    string AuditType,
    int? AuditorId,
    DateOnly? PlannedDate,
    string Status,
    DateTime? StartedAt,
    DateTime? CompletedAt);
