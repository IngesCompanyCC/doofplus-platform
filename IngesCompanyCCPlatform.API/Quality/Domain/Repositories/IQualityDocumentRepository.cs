using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="QualityDocument" />.
/// </summary>
public interface IQualityDocumentRepository : IBaseRepository<QualityDocument>
{
    /// <summary>
    ///     Checks whether a quality document already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
