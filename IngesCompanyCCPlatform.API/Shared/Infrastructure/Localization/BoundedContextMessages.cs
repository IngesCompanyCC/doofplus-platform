namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Localization;

/// <summary>
///     Points to the marker class of the localized messages of a bounded context
///     (for example, <c>SubscriptionsMessages</c> and its <c>.resx</c> files).
/// </summary>
/// <param name="MessagesType">The marker class of the messages.</param>
public sealed record BoundedContextMessages(Type MessagesType);
