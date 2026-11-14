using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of the equipment from the resources.
/// </summary>
public static class EquipmentCommandFromResourceAssembler
{
    private const string CalibrationStatusInvalidMessageKey = "monitoring.equipment.error.calibrationStatus.invalid";

    /// <summary>
    ///     Builds the command to register a piece of equipment.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the code or the calibration status is not valid.</exception>
    public static CreateEquipmentCommand ToCommandFromResource(CreateEquipmentResource resource)
    {
        return new CreateEquipmentCommand(
            new EquipmentCode(resource.Code),
            resource.Name,
            resource.Model ?? string.Empty,
            resource.Location ?? string.Empty,
            EnumParser.ParseOrDefault(resource.CalibrationStatus, ECalibrationStatus.Fit, CalibrationStatusInvalidMessageKey),
            resource.CalibrationDue,
            resource.Certificate ?? string.Empty,
            resource.NextMaintenance,
            resource.MaintenanceOwner ?? string.Empty,
            resource.WorkOrder ?? string.Empty,
            resource.HealthNote ?? string.Empty);
    }

    /// <summary>
    ///     Builds the command to update a piece of equipment.
    /// </summary>
    /// <param name="equipmentId">The id from the route.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the calibration status is not valid.</exception>
    public static UpdateEquipmentCommand ToCommandFromResource(int equipmentId, UpdateEquipmentResource resource)
    {
        return new UpdateEquipmentCommand(
            equipmentId,
            resource.Name,
            resource.Model ?? string.Empty,
            resource.Location ?? string.Empty,
            EnumParser.ParseOrDefault(resource.CalibrationStatus, ECalibrationStatus.Fit, CalibrationStatusInvalidMessageKey),
            resource.CalibrationDue,
            resource.Certificate ?? string.Empty,
            resource.NextMaintenance,
            resource.MaintenanceOwner ?? string.Empty,
            resource.WorkOrder ?? string.Empty,
            resource.HealthNote ?? string.Empty);
    }
}

/// <summary>
///     Builds the resource of a piece of equipment.
/// </summary>
public static class EquipmentResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The equipment.</param>
    /// <returns>The resource.</returns>
    public static EquipmentResource ToResourceFromEntity(Equipment entity)
    {
        return new EquipmentResource(entity.Id.Value, entity.Code.Value, entity.Name, entity.Model, entity.Location,
            entity.CalibrationStatus.ToString(), entity.CalibrationDue, entity.Certificate, entity.NextMaintenance,
            entity.MaintenanceOwner, entity.WorkOrder, entity.HealthNote);
    }
}
