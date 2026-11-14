using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="QualityDocument" />.
/// </summary>
public interface IQualityDocumentQueryService
{
    /// <summary>
    ///     Gets all the quality documents.
    /// </summary>
    Task<IEnumerable<QualityDocument>> Handle(GetAllQualityDocumentsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a quality document by its id, or <c>null</c>.
    /// </summary>
    Task<QualityDocument?> Handle(GetQualityDocumentByIdQuery query, CancellationToken cancellationToken);
}
