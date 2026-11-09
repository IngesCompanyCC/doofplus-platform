using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the quality document query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class QualityDocumentQueryService(IQualityDocumentRepository qualityDocumentRepository)
    : IQualityDocumentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<QualityDocument>> Handle(GetAllQualityDocumentsQuery query,
        CancellationToken cancellationToken)
    {
        return await qualityDocumentRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<QualityDocument?> Handle(GetQualityDocumentByIdQuery query, CancellationToken cancellationToken)
    {
        return await qualityDocumentRepository.FindByIdAsNoTrackingAsync(query.QualityDocumentId, cancellationToken);
    }
}
