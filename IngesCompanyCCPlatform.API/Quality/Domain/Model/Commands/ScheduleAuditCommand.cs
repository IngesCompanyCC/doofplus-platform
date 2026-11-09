using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to schedule an internal or regulatory audit.
/// </summary>
public record ScheduleAuditCommand(EAuditType Type, DateOnly ScheduledDate);
