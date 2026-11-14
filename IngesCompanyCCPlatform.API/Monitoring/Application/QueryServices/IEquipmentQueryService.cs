using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;

/// <summary>
///     Query service of the equipment.
/// </summary>
public interface IEquipmentQueryService
{
    /// <summary>
    ///     Gets all the equipment.
    /// </summary>
    Task<IEnumerable<Equipment>> Handle(GetAllEquipmentQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a piece of equipment by its id, or null.
    /// </summary>
    Task<Equipment?> Handle(GetEquipmentByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a piece of equipment by its code, or null.
    /// </summary>
    Task<Equipment?> Handle(GetEquipmentByCodeQuery query, CancellationToken cancellationToken);
}
