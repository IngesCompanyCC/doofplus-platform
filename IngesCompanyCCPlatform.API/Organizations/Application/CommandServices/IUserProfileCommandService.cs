using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Patterns;

namespace IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;

/// <summary>
///     Application service for UserProfile write use cases.
/// </summary>
public interface IUserProfileCommandService
{
    /// <summary>
    ///     Creates a user profile inside an organization (US55).
    /// </summary>
    Task<Result<UserProfile, string>> Handle(
        CreateUserProfileCommand command,
        CancellationToken cancellationToken = default);
}