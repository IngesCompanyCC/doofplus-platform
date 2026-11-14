using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a audit event.
/// </summary>
public record CreateAuditEventCommand(
    string AggregateType,
    int AggregateId,
    string EventType,
    string Description,
    string PerformedBy);
