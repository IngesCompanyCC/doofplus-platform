using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;

/// <summary>
///     Query service of the sensors.
/// </summary>
public interface ISensorQueryService
{
    /// <summary>
    ///     Gets all the sensors.
    /// </summary>
    Task<IEnumerable<Sensor>> Handle(GetAllSensorsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the sensors of a piece of equipment.
    /// </summary>
    Task<IEnumerable<Sensor>> Handle(GetAllSensorsByEquipmentCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a sensor by its id, or null.
    /// </summary>
    Task<Sensor?> Handle(GetSensorByIdQuery query, CancellationToken cancellationToken);
}
