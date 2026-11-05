using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to create a user profile inside an organization.
/// </summary>
public record CreateUserProfileCommand(
    Guid OrganizationId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Area,
    string Site,
    UserRole Role,
    bool EmailNotifications = true,
    bool InAppNotifications = true,
    string Language = "en");