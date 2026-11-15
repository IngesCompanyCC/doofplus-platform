using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="Evidence" />.
/// </summary>
public interface IEvidenceQueryService
{
    /// <summary>
    ///     Gets the evidence, filtered by record code when it is sent.
    /// </summary>
    Task<IEnumerable<Evidence>> Handle(GetAllEvidenceQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a evidence by its id, or <c>null</c>.
    /// </summary>
    Task<Evidence?> Handle(GetEvidenceByIdQuery query, CancellationToken cancellationToken);
}
