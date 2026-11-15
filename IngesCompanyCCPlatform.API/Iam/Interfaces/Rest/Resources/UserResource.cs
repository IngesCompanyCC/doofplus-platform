namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     A user, with the same fields as the users of the Web Application, plus its roles.
/// </summary>
public record UserResource(
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
    IEnumerable<string> Roles);
