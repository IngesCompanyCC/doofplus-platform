namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A audit event, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record AuditEventResource(
    int Id,
    string EventCode,
    DateTime? OccurredAt,
    string Actor,
    string ActorRole,
    string Event,
    string Record,
    string Result,
    string Field,
    string Before,
    string After,
    string Reason);
