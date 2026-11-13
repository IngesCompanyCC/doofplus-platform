using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;

/// <summary>
///     Command service of the master formulas.
/// </summary>
public interface IMasterFormulaCommandService
{
    /// <summary>
    ///     Writes a new version of the formula of an existing product, in Draft.
    /// </summary>
    Task<Result<MasterFormula>> Handle(CreateMasterFormulaCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Approves a formula in Draft; the version approved before becomes obsolete.
    /// </summary>
    Task<Result<MasterFormula>> Handle(ApproveMasterFormulaCommand command, CancellationToken cancellationToken);
}
