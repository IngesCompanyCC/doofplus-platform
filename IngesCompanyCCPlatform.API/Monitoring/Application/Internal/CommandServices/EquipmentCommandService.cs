using IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.CommandServices;

/// <summary>
///     Command service of the equipment.
/// </summary>
public class EquipmentCommandService(
    IEquipmentRepository equipmentRepository,
    ISensorRepository sensorRepository,
    IUnitOfWork unitOfWork) : IEquipmentCommandService
{
    /// <inheritdoc />
    public async Task<Result<Equipment>> Handle(CreateEquipmentCommand command, CancellationToken cancellationToken)
    {
        if (await equipmentRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<Equipment>.Failure(EquipmentErrors.DuplicateCode, MonitoringError.DuplicateEquipmentCode);

        var equipment = new Equipment(command);
        await equipmentRepository.AddAsync(equipment, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Equipment>.Success(equipment);
    }

    /// <inheritdoc />
    public async Task<Result<Equipment>> Handle(UpdateEquipmentCommand command, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.FindByIdAsync(command.EquipmentId, cancellationToken);
        if (equipment is null)
            return Result<Equipment>.Failure(EquipmentErrors.NotFoundById(command.EquipmentId),
                MonitoringError.EquipmentNotFound);

        equipment.Update(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Equipment>.Success(equipment);
    }

    /// <inheritdoc />
    public async Task<Result<Equipment>> Handle(DeleteEquipmentCommand command, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.FindByIdAsync(command.EquipmentId, cancellationToken);
        if (equipment is null)
            return Result<Equipment>.Failure(EquipmentErrors.NotFoundById(command.EquipmentId),
                MonitoringError.EquipmentNotFound);
        if (await sensorRepository.ExistsAssignedToBatchByEquipmentCodeAsync(equipment.Code, cancellationToken))
            return Result<Equipment>.Failure(EquipmentErrors.InUse, MonitoringError.EquipmentInUse);

        equipmentRepository.Remove(equipment);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Equipment>.Success(equipment);
    }
}
