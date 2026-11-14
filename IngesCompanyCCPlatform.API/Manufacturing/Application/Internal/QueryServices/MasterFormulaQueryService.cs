using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="MasterFormula" />.
/// </summary>
public class MasterFormulaQueryService(IMasterFormulaRepository masterFormulaRepository) : IMasterFormulaQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<MasterFormula>> Handle(GetAllMasterFormulasQuery query, CancellationToken cancellationToken)
    {
        return await masterFormulaRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MasterFormula>> Handle(GetAllMasterFormulasByProductCodeQuery query, CancellationToken cancellationToken)
    {
        return await masterFormulaRepository.FindAllByProductCodeAsNoTrackingAsync(query.ProductCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MasterFormula?> Handle(GetMasterFormulaByIdQuery query, CancellationToken cancellationToken)
    {
        return await masterFormulaRepository.FindByIdAsNoTrackingAsync(query.MasterFormulaId, cancellationToken);
    }
}
