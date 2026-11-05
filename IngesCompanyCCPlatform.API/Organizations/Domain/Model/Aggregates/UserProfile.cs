using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     UserProfile aggregate root.
///     Personal data and preferences of a user that belongs to an organization (US55).
///     Authentication data lives in the IAM bounded context (UserId references IAM).
/// </summary>
public class UserProfile : IAuditableEntity
{
    public Guid Id { get; private set; }
    public Guid OrganizationId { get; private set; }
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Area { get; private set; } = string.Empty;
    public string Site { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }
    public bool EmailNotifications { get; private set; }
    public bool InAppNotifications { get; private set; }
    public string Language { get; private set; } = "en";
    public bool IsActive { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}".Trim();

    private UserProfile()
    {
    }

    public UserProfile(CreateUserProfileCommand command)
    {
        Id = Guid.NewGuid();
        OrganizationId = command.OrganizationId;
        UserId = command.UserId;
        FirstName = command.FirstName.Trim();
        LastName = command.LastName.Trim();
        Email = command.Email.Trim().ToLowerInvariant();
        Area = command.Area.Trim();
        Site = command.Site.Trim();
        Role = command.Role;
        EmailNotifications = command.EmailNotifications;
        InAppNotifications = command.InAppNotifications;
        Language = string.IsNullOrWhiteSpace(command.Language)
            ? "en"
            : command.Language.Trim().ToLowerInvariant();
        IsActive = true;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
    }

    public void UpdatePreferences(bool emailNotifications, bool inAppNotifications, string language)
    {
        EmailNotifications = emailNotifications;
        InAppNotifications = inAppNotifications;

        if (!string.IsNullOrWhiteSpace(language))
            Language = language.Trim().ToLowerInvariant();
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public UserProfileCreatedEvent ToCreatedEvent()
    {
        return new UserProfileCreatedEvent(Id, OrganizationId, UserId, Email, Role);
    }
}