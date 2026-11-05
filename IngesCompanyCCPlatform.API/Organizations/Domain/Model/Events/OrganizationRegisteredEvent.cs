namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Events;

/// <summary>
///     Raised when a new organization is registered successfully.
///     IAM can listen to this event to create the administrator account.
/// </summary>
public record OrganizationRegisteredEvent(
    Guid OrganizationId,
    string LegalName,
    string Ruc,
    string AdministratorEmail,
    string AdministratorName,
    string Password);