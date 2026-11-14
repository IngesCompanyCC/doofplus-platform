namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record UserProfileResource(
    int Id,
    int UserId,
    int OrganizationId,
    string FirstName,
    string LastName,
    string Email,
    string Area,
    string Site,
    bool EmailNotifications,
    bool InAppNotifications,
    string Language);