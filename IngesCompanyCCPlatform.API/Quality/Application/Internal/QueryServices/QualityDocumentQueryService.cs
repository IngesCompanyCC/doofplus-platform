using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="QualityDocument" />.
/// </summary>
public class QualityDocumentQueryService(IQualityDocumentRepository qualityDocumentRepository) : IQualityDocumentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<QualityDocument>> Handle(GetAllQualityDocumentsQuery query, CancellationToken cancellationToken)
    {
        return string.IsNullOrWhiteSpace(query.Code)
            ? await qualityDocumentRepository.ListAsNoTrackingAsync(cancellationToken)
            : await qualityDocumentRepository.FindAllByCodeAsNoTrackingAsync(query.Code.Trim(), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<QualityDocument?> Handle(GetQualityDocumentByIdQuery query, CancellationToken cancellationToken)
    {
        return await qualityDocumentRepository.FindByIdAsNoTrackingAsync(query.QualityDocumentId, cancellationToken);
    }
}
