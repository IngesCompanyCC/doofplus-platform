using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class UserProfileResourceFromEntityAssembler
{
    public static UserProfileResource ToResourceFromEntity(UserProfile entity)
    {
        return new UserProfileResource(
            entity.Id,
            entity.UserId,
            entity.OrganizationId,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.Area,
            entity.Site,
            entity.EmailNotifications,
            entity.InAppNotifications,
            entity.Language);
    }
}