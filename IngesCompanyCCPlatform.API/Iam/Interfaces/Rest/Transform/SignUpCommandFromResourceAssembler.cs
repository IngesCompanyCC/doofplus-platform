using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource) =>
        new(resource.Email, resource.Password, resource.FullName, resource.Role, resource.Environment,
            resource.Facility, resource.Status, resource.Privilege, resource.OrganizationId,
            resource.OrganizationName, resource.Plant, resource.TwoFactorCode);
}
