using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token) =>
        new(user.Id, user.FullName, user.Initials, user.Email, user.Role.ToKey(), user.Environment, user.Facility,
            user.Status, user.Privilege, user.OrganizationId, user.OrganizationName, user.Plant, token);
}
