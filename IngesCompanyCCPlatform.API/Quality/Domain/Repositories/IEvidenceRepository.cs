using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

/// <summary>
///     Repository of <see cref="Evidence" />.
/// </summary>
public interface IEvidenceRepository : IBaseRepository<Evidence>
{
    /// <summary>
    ///     Gets the evidence with the record code.
    /// </summary>
    Task<IEnumerable<Evidence>> FindAllByRecordCodeAsNoTrackingAsync(string recordCode, CancellationToken cancellationToken = default);
}
