using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="CapaPlan" />.
/// </summary>
public class CapaPlanQueryService(ICapaPlanRepository capaPlanRepository) : ICapaPlanQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<CapaPlan>> Handle(GetAllCapaPlansQuery query, CancellationToken cancellationToken)
    {
        return await capaPlanRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CapaPlan?> Handle(GetCapaPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await capaPlanRepository.FindByIdAsNoTrackingAsync(query.CapaPlanId, cancellationToken);
    }
}
