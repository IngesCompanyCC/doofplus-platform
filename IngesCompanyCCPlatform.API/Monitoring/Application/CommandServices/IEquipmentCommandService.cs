using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;

/// <summary>
///     Command service of the equipment.
/// </summary>
public interface IEquipmentCommandService
{
    /// <summary>
    ///     Registers a piece of equipment; fails if the code already exists.
    /// </summary>
    Task<Result<Equipment>> Handle(CreateEquipmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates a piece of equipment; fails if it does not exist.
    /// </summary>
    Task<Result<Equipment>> Handle(UpdateEquipmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Removes a piece of equipment; fails if it does not exist or is in use.
    /// </summary>
    Task<Result<Equipment>> Handle(DeleteEquipmentCommand command, CancellationToken cancellationToken);
}
