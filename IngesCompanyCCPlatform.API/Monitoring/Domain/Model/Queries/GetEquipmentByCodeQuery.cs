using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get a piece of equipment by its code.
/// </summary>
public record GetEquipmentByCodeQuery(EquipmentCode EquipmentCode);
