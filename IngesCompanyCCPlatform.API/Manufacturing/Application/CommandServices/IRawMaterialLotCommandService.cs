using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;

/// <summary>
///     Command service of the lots of raw material.
/// </summary>
public interface IRawMaterialLotCommandService
{
    /// <summary>
    ///     Receives a lot, in Inspection.
    /// </summary>
    Task<Result<RawMaterialLot>> Handle(ReceiveRawMaterialLotCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Records the incoming inspection of a lot.
    /// </summary>
    Task<Result<RawMaterialLot>> Handle(InspectRawMaterialLotCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Releases a lot that passed its inspection.
    /// </summary>
    Task<Result<RawMaterialLot>> Handle(ApproveRawMaterialLotCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Rejects a lot in inspection.
    /// </summary>
    Task<Result<RawMaterialLot>> Handle(RejectRawMaterialLotCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Allocates an approved lot to a production batch.
    /// </summary>
    Task<Result<RawMaterialLot>> Handle(AllocateRawMaterialLotCommand command, CancellationToken cancellationToken);
}
