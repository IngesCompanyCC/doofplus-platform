namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to update the notification preferences and the language of a user profile.
/// </summary>
/// <param name="ProfileId">The identifier of the user profile.</param>
/// <param name="EmailNotifications">Whether the user receives email notifications; <c>null</c> keeps the current value.</param>
/// <param name="InAppNotifications">Whether the user receives in-app notifications; <c>null</c> keeps the current value.</param>
/// <param name="Language">The language of the user, for example <c>en</c> or <c>es</c>; blank keeps the current value.</param>
public record UpdateUserProfilePreferencesCommand(
    int ProfileId,
    bool? EmailNotifications,
    bool? InAppNotifications,
    string? Language);
