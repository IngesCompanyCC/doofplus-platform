using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Command service of the lots of raw material.
/// </summary>
public class RawMaterialLotCommandService(IRawMaterialLotRepository rawMaterialLotRepository, IUnitOfWork unitOfWork)
    : IRawMaterialLotCommandService
{
    /// <inheritdoc />
    public async Task<Result<RawMaterialLot>> Handle(ReceiveRawMaterialLotCommand command,
        CancellationToken cancellationToken)
    {
        if (await rawMaterialLotRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<RawMaterialLot>.Failure(RawMaterialLotErrors.DuplicateCode,
                ManufacturingError.DuplicateRawMaterialLotCode);

        var lot = new RawMaterialLot(command);
        await rawMaterialLotRepository.AddAsync(lot, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RawMaterialLot>.Success(lot);
    }

    /// <inheritdoc />
    public Task<Result<RawMaterialLot>> Handle(InspectRawMaterialLotCommand command,
        CancellationToken cancellationToken)
    {
        return Transition(command.RawMaterialLotId, lot => lot.Inspect(command), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<RawMaterialLot>> Handle(ApproveRawMaterialLotCommand command,
        CancellationToken cancellationToken)
    {
        return Transition(command.RawMaterialLotId, lot => lot.Approve(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<RawMaterialLot>> Handle(RejectRawMaterialLotCommand command,
        CancellationToken cancellationToken)
    {
        return Transition(command.RawMaterialLotId, lot => lot.Reject(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<RawMaterialLot>> Handle(AllocateRawMaterialLotCommand command,
        CancellationToken cancellationToken)
    {
        return Transition(command.RawMaterialLotId, lot => lot.AllocateTo(command.BatchCode), cancellationToken);
    }

    /// <summary>
    ///     Finds the lot, applies the change and saves it; a change not allowed from its status is a conflict.
    /// </summary>
    private async Task<Result<RawMaterialLot>> Transition(int lotId, Action<RawMaterialLot> change,
        CancellationToken cancellationToken)
    {
        var lot = await rawMaterialLotRepository.FindByIdAsync(lotId, cancellationToken);
        if (lot is null)
            return Result<RawMaterialLot>.Failure(RawMaterialLotErrors.NotFoundById(lotId),
                ManufacturingError.RawMaterialLotNotFound);

        try
        {
            change(lot);
        }
        catch (InvalidOperationException exception)
        {
            return Result<RawMaterialLot>.Failure(RawMaterialLotErrors.InvalidState(exception.Message),
                ManufacturingError.InvalidRawMaterialLotState);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<RawMaterialLot>.Success(lot);
    }
}
