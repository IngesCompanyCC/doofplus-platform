using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get a piece of equipment by its id.
/// </summary>
public record GetEquipmentByIdQuery(EquipmentId EquipmentId);
