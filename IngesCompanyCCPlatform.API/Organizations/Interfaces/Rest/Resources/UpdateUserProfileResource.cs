namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

/// <summary>
///     Resource to update the notification preferences and the language of a user profile.
/// </summary>
/// <param name="EmailNotifications">Whether the user receives email notifications.</param>
/// <param name="InAppNotifications">Whether the user receives in-app notifications.</param>
/// <param name="Language">The language of the user, for example <c>en</c> or <c>es</c>.</param>
public record UpdateUserProfileResource(bool? EmailNotifications, bool? InAppNotifications, string? Language);
