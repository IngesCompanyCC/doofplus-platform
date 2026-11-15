using System.Net.Mail;
using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="Invitation" />. An email of a user who is no longer invited, or that already
///     has a pending invitation, cannot be invited again.
/// </summary>
public class InvitationCommandService(
    IInvitationRepository invitationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IInvitationCommandService
{
    /// <inheritdoc />
    public async Task<Result<Invitation>> Handle(SendInvitationCommand command, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.FullName) || string.IsNullOrWhiteSpace(command.Facility)
            || !MailAddress.TryCreate(command.Email?.Trim(), out _))
            return Result<Invitation>.Failure(IamError.InvalidUserData, "The full name, a valid email and the facility are required.");
        if (!RolesExtensions.TryFromKey(command.Role, out var role))
            return Result<Invitation>.Failure(IamError.InvalidUserData, "The role is not valid.");

        // US55: the account of an invited member exists in the Invited status until the invitation is accepted.
        var email = command.Email!.Trim().ToLowerInvariant();
        var user = await userRepository.FindByEmailAsync(email, cancellationToken);
        if ((user is not null && user.Status != "invited")
            || await invitationRepository.ExistsPendingByEmailAsync(email, cancellationToken))
            return Result<Invitation>.Failure(IamError.EmailAlreadyTaken, "A user or a pending invitation already uses the email.");

        var invitation = new Invitation(command, role);
        await invitationRepository.AddAsync(invitation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Invitation>.Success(invitation);
    }
}
