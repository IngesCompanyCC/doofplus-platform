using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the readings of a sensor.
/// </summary>
public record GetAllReadingsBySensorCodeQuery(SensorCode SensorCode);
