using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a audit event.
/// </summary>
public record CreateAuditEventCommand(
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
