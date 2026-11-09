using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the quality document query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IQualityDocumentQueryService
{
    Task<IEnumerable<QualityDocument>> Handle(GetAllQualityDocumentsQuery query, CancellationToken cancellationToken);

    Task<QualityDocument?> Handle(GetQualityDocumentByIdQuery query, CancellationToken cancellationToken);
}
