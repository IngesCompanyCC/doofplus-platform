using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Patterns;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.CommandServices;

/// <summary>
///     Handles user profile write use cases (US55).
/// </summary>
public class UserProfileCommandService(
    IUserProfileRepository userProfileRepository,
    IOrganizationRepository organizationRepository,
    IUnitOfWork unitOfWork) : IUserProfileCommandService
{
    public async Task<Result<UserProfile, string>> Handle(
        CreateUserProfileCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.OrganizationId == Guid.Empty)
            return new Result<UserProfile, string>.Failure("Organization id is required.");

        if (command.UserId == Guid.Empty)
            return new Result<UserProfile, string>.Failure("User id is required.");

        if (string.IsNullOrWhiteSpace(command.FirstName))
            return new Result<UserProfile, string>.Failure("First name is required.");

        if (string.IsNullOrWhiteSpace(command.LastName))
            return new Result<UserProfile, string>.Failure("Last name is required.");

        if (string.IsNullOrWhiteSpace(command.Email))
            return new Result<UserProfile, string>.Failure("Email is required.");

        var organization = await organizationRepository.FindByIdAsync(
            command.OrganizationId,
            cancellationToken);

        if (organization is null)
            return new Result<UserProfile, string>.Failure("Organization was not found.");

        var normalizedEmail = command.Email.Trim().ToLowerInvariant();

        if (await userProfileRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken))
            return new Result<UserProfile, string>.Failure("A user profile with this email already exists.");

        var profile = new UserProfile(command);

        await userProfileRepository.AddAsync(profile, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Domain event available for later integration:
        // var domainEvent = profile.ToCreatedEvent();

        return new Result<UserProfile, string>.Success(profile);
    }
}