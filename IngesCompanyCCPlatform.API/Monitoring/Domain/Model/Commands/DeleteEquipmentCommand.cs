using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;

/// <summary>
///     Command to remove a piece of equipment that is not in use.
/// </summary>
public record DeleteEquipmentCommand(
    EquipmentId EquipmentId);
