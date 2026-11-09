using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Represents the quality document repository of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IQualityDocumentRepository : IBaseRepository<QualityDocument>
{
    /// <summary>
    ///     Checks whether a quality document with the given code exists.
    /// </summary>
    /// <param name="code">The code of the document.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when a document already uses the code.</returns>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
