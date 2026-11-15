namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a audit event. Missing optional values take their default.
/// </summary>
public record CreateAuditEventResource(
    string? EventCode,
    DateTime? OccurredAt,
    string? Actor,
    string? ActorRole,
    string? Event,
    string? Record,
    string? Result,
    string? Field,
    string? Before,
    string? After,
    string? Reason);
