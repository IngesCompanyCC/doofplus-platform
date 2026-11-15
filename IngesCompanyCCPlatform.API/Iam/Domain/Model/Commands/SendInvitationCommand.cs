namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command to invite a new member of the laboratory with a role and a facility.
/// </summary>
/// <param name="OrganizationId">The organization that invites.</param>
/// <param name="Facility">The facility the person will work in.</param>
/// <param name="Role">The role key, for example internal-auditor.</param>
/// <param name="FullName">The full name of the person.</param>
/// <param name="Email">The work email that receives the invitation.</param>
/// <param name="Note">An optional note for the person.</param>
public record SendInvitationCommand(int OrganizationId, string Facility, string Role, string FullName, string Email, string? Note);
