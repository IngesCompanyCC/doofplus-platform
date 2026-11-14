using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get an alert by its id.
/// </summary>
public record GetAlertByIdQuery(AlertId AlertId);
