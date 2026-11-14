using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="RawMaterialLot" />.
/// </summary>
public class RawMaterialLotQueryService(IRawMaterialLotRepository rawMaterialLotRepository) : IRawMaterialLotQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<RawMaterialLot>> Handle(GetAllRawMaterialLotsQuery query, CancellationToken cancellationToken)
    {
        return await rawMaterialLotRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<RawMaterialLot?> Handle(GetRawMaterialLotByIdQuery query, CancellationToken cancellationToken)
    {
        return await rawMaterialLotRepository.FindByIdAsNoTrackingAsync(query.RawMaterialLotId, cancellationToken);
    }
}
