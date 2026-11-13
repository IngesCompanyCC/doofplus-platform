using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class CreateUserProfileCommandFromResourceAssembler
{
    public static CreateUserProfileCommand ToCommandFromResource(
        CreateUserProfileResource resource)
    {
        var role = Enum.TryParse<UserRole>(resource.Role, ignoreCase: true, out var parsedRole)
            ? parsedRole
            : UserRole.QaSpecialist;

        return new CreateUserProfileCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Area,
            resource.Site,
            role,
            resource.EmailNotifications,
            resource.InAppNotifications,
            resource.Language);
    }
}