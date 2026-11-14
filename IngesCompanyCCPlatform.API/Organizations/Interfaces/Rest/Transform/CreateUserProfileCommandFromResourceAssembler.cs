using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class CreateUserProfileCommandFromResourceAssembler
{
    public static CreateUserProfileCommand ToCommandFromResource(
        CreateUserProfileResource resource)
    {

        return new CreateUserProfileCommand(
            resource.OrganizationId,
            resource.UserId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Area,
            resource.Site,
            UserRole.QaSpecialist,
            true,
            true,
            resource.Language);
    }
}