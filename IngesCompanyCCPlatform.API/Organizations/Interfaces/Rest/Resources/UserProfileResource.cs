namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record UserProfileResource(
    int Id,
    int OrganizationId,
    int UserId,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Area,
    string Site,
    string Role,
    bool EmailNotifications,
    bool InAppNotifications,
    string Language,
    bool IsActive,
    DateTimeOffset? CreatedAt);