using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Transform;

/// <summary>
///     Builds the command to send an invitation from its resource.
/// </summary>
public static class SendInvitationCommandFromResourceAssembler
{
    public static SendInvitationCommand ToCommandFromResource(SendInvitationResource resource) =>
        new(resource.OrganizationId, resource.Facility ?? string.Empty, resource.Role ?? string.Empty,
            resource.FullName ?? string.Empty, resource.Email ?? string.Empty, resource.Note);
}

/// <summary>
///     Builds the resource of an invitation.
/// </summary>
public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation invitation) =>
        new(invitation.Id, invitation.OrganizationId, invitation.FullName, invitation.Email, invitation.Role.ToKey(),
            invitation.Facility, invitation.Note, invitation.Status.ToString(),
            DateTime.SpecifyKind(invitation.SentAt, DateTimeKind.Utc));
}
