namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     The signed-in user and the JWT to send as Authorization: Bearer &lt;token&gt;.
/// </summary>
public record AuthenticatedUserResource(
    int Id,
    string FullName,
    string Initials,
    string Email,
    string Role,
    string Environment,
    string Facility,
    string Status,
    string Privilege,
    int OrganizationId,
    string OrganizationName,
    string Plant,
    string Token);
