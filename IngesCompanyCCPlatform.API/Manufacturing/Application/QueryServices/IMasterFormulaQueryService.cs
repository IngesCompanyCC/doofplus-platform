using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;

/// <summary>
///     Query service of the master formulas.
/// </summary>
public interface IMasterFormulaQueryService
{
    /// <summary>
    ///     Gets all the formulas with their lines.
    /// </summary>
    Task<IEnumerable<MasterFormula>> Handle(GetAllMasterFormulasQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the formulas of a product.
    /// </summary>
    Task<IEnumerable<MasterFormula>> Handle(GetAllMasterFormulasByProductCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a formula by its id, or null.
    /// </summary>
    Task<MasterFormula?> Handle(GetMasterFormulaByIdQuery query, CancellationToken cancellationToken);
}
