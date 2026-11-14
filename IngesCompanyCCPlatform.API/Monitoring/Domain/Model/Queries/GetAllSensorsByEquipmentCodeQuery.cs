using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get the sensors installed on a piece of equipment.
/// </summary>
public record GetAllSensorsByEquipmentCodeQuery(EquipmentCode EquipmentCode);
