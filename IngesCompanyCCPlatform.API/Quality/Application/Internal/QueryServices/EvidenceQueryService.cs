using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Evidence" />.
/// </summary>
public class EvidenceQueryService(IEvidenceRepository evidenceRepository) : IEvidenceQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Evidence>> Handle(GetAllEvidenceQuery query, CancellationToken cancellationToken)
    {
        return await evidenceRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Evidence?> Handle(GetEvidenceByIdQuery query, CancellationToken cancellationToken)
    {
        return await evidenceRepository.FindByIdAsNoTrackingAsync(query.EvidenceId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<Evidence>> Handle(GetAllEvidenceByDeviationIdQuery query, CancellationToken cancellationToken)
    {
        return await evidenceRepository.FindAllByDeviationIdAsNoTrackingAsync(query.DeviationId, cancellationToken);
    }
}
