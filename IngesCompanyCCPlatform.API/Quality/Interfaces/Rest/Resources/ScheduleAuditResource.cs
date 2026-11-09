namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to schedule an audit. Type: Internal or Regulatory.
/// </summary>
public record ScheduleAuditResource(string Type, DateOnly ScheduledDate);
