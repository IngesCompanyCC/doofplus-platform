using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Events;

/// <summary>
///     Raised when a user profile is created inside an organization.
/// </summary>
public record UserProfileCreatedEvent(
    int ProfileId,
    int OrganizationId,
    int UserId,
    string Email,
    UserRole Role);