using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the alerts raised during a production batch.
/// </summary>
public record GetAllAlertsByBatchCodeQuery(string BatchCode);
