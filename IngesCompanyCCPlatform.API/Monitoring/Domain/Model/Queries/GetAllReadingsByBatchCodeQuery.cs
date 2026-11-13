using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the readings recorded during a production batch.
/// </summary>
public record GetAllReadingsByBatchCodeQuery(string BatchCode);
