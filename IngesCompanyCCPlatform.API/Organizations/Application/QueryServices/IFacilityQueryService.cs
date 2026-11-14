using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;

/// <summary>
///     Query service of the facilities.
/// </summary>
public interface IFacilityQueryService
{
    Task<IEnumerable<Facility>> Handle(GetAllFacilitiesQuery query, CancellationToken cancellationToken = default);
}
