namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command to register a user of an organization, with the same fields as the users of the Web Application.
///     <c>Role</c> is the kebab-case key (qa-specialist); empty means qa-specialist.
/// </summary>
public record SignUpCommand(
    string Email,
    string Password,
    string FullName,
    string? Role,
    string Environment,
    string? Facility,
    string? Status,
    string? Privilege,
    int OrganizationId,
    string? OrganizationName,
    string? Plant,
    string? TwoFactorCode);
