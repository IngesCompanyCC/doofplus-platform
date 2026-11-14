namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Data to register a user. Role: administrator, qa-specialist, production-supervisor, quality-manager or
///     internal-auditor. Environment: qa, production or administration.
/// </summary>
public record SignUpResource(
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
