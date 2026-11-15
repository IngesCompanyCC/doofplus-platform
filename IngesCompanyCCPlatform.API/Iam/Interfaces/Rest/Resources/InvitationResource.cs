namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     An invitation, with the fields of the invitations of the Web Application. Role is the role key
///     (for example internal-auditor); status is Pending, Accepted or Expired.
/// </summary>
public record InvitationResource(
    int Id,
    int OrganizationId,
    string FullName,
    string Email,
    string Role,
    string Facility,
    string Note,
    string Status,
    DateTime SentAt);
