namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record UserProfileResource(
    Guid Id,
    Guid OrganizationId,
    Guid UserId,
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