namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Data to invite a new member: role is a role key (administrator, qa-specialist, production-supervisor,
///     quality-manager or internal-auditor).
/// </summary>
public record SendInvitationResource(
    int OrganizationId,
    string FullName,
    string Email,
    string Role,
    string Facility,
    string? Note);
