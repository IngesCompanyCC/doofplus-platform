using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
///     Handles user profile read use cases.
/// </summary>
public class UserProfileQueryService(
    IUserProfileRepository userProfileRepository) : IUserProfileQueryService
{
    public async Task<UserProfile?> Handle(
        GetUserProfileByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await userProfileRepository.FindByIdAsync(query.ProfileId, cancellationToken);
    }

    public async Task<IEnumerable<UserProfile>> Handle(
        GetUserProfilesByOrganizationIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await userProfileRepository.FindByOrganizationIdAsync(
            query.OrganizationId,
            cancellationToken);
    }
}