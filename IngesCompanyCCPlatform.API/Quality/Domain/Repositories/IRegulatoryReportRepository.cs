using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="RegulatoryReport" />.
/// </summary>
public interface IRegulatoryReportRepository : IBaseRepository<RegulatoryReport>
{
    /// <summary>
    ///     Checks whether a regulatory report already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
