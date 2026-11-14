using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;

/// <summary>
///     Query service of the lots of raw material.
/// </summary>
public interface IRawMaterialLotQueryService
{
    /// <summary>
    ///     Gets all the lots.
    /// </summary>
    Task<IEnumerable<RawMaterialLot>> Handle(GetAllRawMaterialLotsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a lot by its id, or null.
    /// </summary>
    Task<RawMaterialLot?> Handle(GetRawMaterialLotByIdQuery query, CancellationToken cancellationToken);
}
