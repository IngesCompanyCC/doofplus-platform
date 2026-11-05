using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;

/// <summary>
///     Application service for UserProfile read use cases.
/// </summary>
public interface IUserProfileQueryService
{
    Task<UserProfile?> Handle(
        GetUserProfileByIdQuery query,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<UserProfile>> Handle(
        GetUserProfilesByOrganizationIdQuery query,
        CancellationToken cancellationToken = default);
}