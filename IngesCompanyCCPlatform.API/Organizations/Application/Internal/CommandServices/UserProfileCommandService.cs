using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
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
    public async Task<Result<UserProfile>> Handle(
        CreateUserProfileCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.OrganizationId <= 0)
            return Result<UserProfile>.Failure(OrganizationsError.InvalidOperation, "Organization id is required.");

        if (command.UserId <= 0)
            return Result<UserProfile>.Failure(OrganizationsError.InvalidOperation, "User id is required.");

        if (string.IsNullOrWhiteSpace(command.FirstName))
            return Result<UserProfile>.Failure(OrganizationsError.InvalidOperation, "First name is required.");

        if (string.IsNullOrWhiteSpace(command.LastName))
            return Result<UserProfile>.Failure(OrganizationsError.InvalidOperation, "Last name is required.");

        if (string.IsNullOrWhiteSpace(command.Email))
            return Result<UserProfile>.Failure(OrganizationsError.InvalidOperation, "Email is required.");

        var organization = await organizationRepository.FindByIdAsync(
            command.OrganizationId,
            cancellationToken);

        if (organization is null)
            return Result<UserProfile>.Failure(OrganizationsError.OrganizationNotFound, "Organization was not found.");

        var normalizedEmail = command.Email.Trim().ToLowerInvariant();

        if (await userProfileRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken))
            return Result<UserProfile>.Failure(OrganizationsError.DuplicateUserEmail, "A user profile with this email already exists.");

        var profile = new UserProfile(command);

        await userProfileRepository.AddAsync(profile, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Domain event available for later integration:
        // var domainEvent = profile.ToCreatedEvent();

        return Result<UserProfile>.Success(profile);
    }
}
