using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class UserProfileResourceFromEntityAssembler
{
    public static UserProfileResource ToResourceFromEntity(UserProfile entity)
    {
        return new UserProfileResource(
            entity.Id,
            entity.OrganizationId,
            entity.UserId,
            entity.FirstName,
            entity.LastName,
            entity.FullName,
            entity.Email,
            entity.Area,
            entity.Site,
            entity.Role.ToString(),
            entity.EmailNotifications,
            entity.InAppNotifications,
            entity.Language,
            entity.IsActive,
            entity.CreatedAt);
    }
}