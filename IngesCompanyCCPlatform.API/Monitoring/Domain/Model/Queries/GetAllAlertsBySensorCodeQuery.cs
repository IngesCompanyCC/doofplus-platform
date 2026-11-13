using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the alerts raised by a sensor.
/// </summary>
public record GetAllAlertsBySensorCodeQuery(SensorCode SensorCode);
