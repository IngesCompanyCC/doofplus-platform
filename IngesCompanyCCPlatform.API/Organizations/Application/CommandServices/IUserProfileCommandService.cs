using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;

/// <summary>
///     Application service for UserProfile write use cases.
/// </summary>
public interface IUserProfileCommandService
{
    /// <summary>
    ///     Creates a user profile inside an organization (US55).
    /// </summary>
    Task<Result<UserProfile>> Handle(
        CreateUserProfileCommand command,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles the command to update the notification preferences and the language of a user profile.
    /// </summary>
    Task<Result<UserProfile>> Handle(
        UpdateUserProfilePreferencesCommand command,
        CancellationToken cancellationToken = default);
}