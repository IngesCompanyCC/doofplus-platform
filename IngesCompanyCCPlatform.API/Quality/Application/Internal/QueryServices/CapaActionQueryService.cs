using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="CapaAction" />.
/// </summary>
public class CapaActionQueryService(ICapaActionRepository capaActionRepository) : ICapaActionQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<CapaAction>> Handle(GetAllCapaActionsQuery query, CancellationToken cancellationToken)
    {
        return string.IsNullOrWhiteSpace(query.CapaCode)
            ? await capaActionRepository.ListAsNoTrackingAsync(cancellationToken)
            : await capaActionRepository.FindAllByCapaCodeAsNoTrackingAsync(query.CapaCode.Trim(), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CapaAction?> Handle(GetCapaActionByIdQuery query, CancellationToken cancellationToken)
    {
        return await capaActionRepository.FindByIdAsNoTrackingAsync(query.CapaActionId, cancellationToken);
    }
}
