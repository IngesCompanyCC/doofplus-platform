using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     The password and the two-factor code never leave the API.
/// </summary>
public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user) =>
        new(user.Id, user.FullName, user.Initials, user.Email, user.Role.ToKey(), user.Environment, user.Facility,
            user.Status, user.Privilege, user.OrganizationId, user.OrganizationName, user.Plant,
            [user.Role.ToName()]);
}
