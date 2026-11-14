using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="Audit" />.
/// </summary>
public interface IAuditRepository : IBaseRepository<Audit>
{
    /// <summary>
    ///     Checks whether a audit already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
