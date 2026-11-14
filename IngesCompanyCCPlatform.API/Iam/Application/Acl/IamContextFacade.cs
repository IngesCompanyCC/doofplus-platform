using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Acl;

namespace IngesCompanyCCPlatform.API.Iam.Application.Acl;

public class IamContextFacade(IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByEmailQuery(email.Trim().ToLowerInvariant()), cancellationToken);
        return user?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return user?.Email ?? string.Empty;
    }

    public async Task<int> FetchOrganizationIdByUserId(int userId, CancellationToken cancellationToken = default)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId), cancellationToken);
        return user?.OrganizationId ?? 0;
    }
}
