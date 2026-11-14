using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get a sensor by its id.
/// </summary>
public record GetSensorByIdQuery(SensorId SensorId);
