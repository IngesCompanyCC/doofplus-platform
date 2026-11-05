namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record CreateUserProfileResource(
    Guid OrganizationId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Area,
    string Site,
    string Role,
    bool EmailNotifications = true,
    bool InAppNotifications = true,
    string Language = "en");