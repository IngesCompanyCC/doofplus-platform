using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="QualityDocument" />.
/// </summary>
public interface IQualityDocumentRepository : IBaseRepository<QualityDocument>
{
    /// <summary>
    ///     Gets the quality documents with the code.
    /// </summary>
    Task<IEnumerable<QualityDocument>> FindAllByCodeAsNoTrackingAsync(string code, CancellationToken cancellationToken = default);
}
