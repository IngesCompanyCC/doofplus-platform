using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;

/// <summary>
///     Repository of the master formulas.
/// </summary>
public interface IMasterFormulaRepository : IBaseRepository<MasterFormula>
{
    /// <summary>
    ///     Checks whether the product already has a formula with the version.
    /// </summary>
    Task<bool> ExistsByProductCodeAndVersionAsync(ProductCode productCode, string version, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the formulas of a product, tracked, to change their status.
    /// </summary>
    Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsync(ProductCode productCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the formulas of a product with their lines.
    /// </summary>
    Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsNoTrackingAsync(ProductCode productCode, CancellationToken cancellationToken = default);
}
