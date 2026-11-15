using IngesCompanyCCPlatform.API.Iam.Application.CommandServices;
using IngesCompanyCCPlatform.API.Iam.Application.Internal.OutboundServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Iam.Application.Internal.CommandServices;

/// <summary>
///     Sign-in with two-factor authentication and sign-up of the users.
/// </summary>
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork) : IUserCommandService
{
    /// <summary>Detail of the 401 when the email or the password is wrong.</summary>
    public const string InvalidCredentials = "invalid-credentials";

    /// <summary>Detail of the 401 when the password is right but the two-factor code is missing.</summary>
    public const string TwoFactorRequired = "two-factor-required";

    /// <summary>Detail of the 401 when the two-factor code is wrong.</summary>
    public const string InvalidTwoFactorCode = "invalid-two-factor-code";

    private const int MinPasswordLength = 8;

    /// <summary>
    ///     Issues the token only after the password and, when the user enrolled it, the two-factor code are verified.
    ///     Every failure answers 401 with its reason, without telling whether the email exists.
    /// </summary>
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = command.Email?.Trim().ToLowerInvariant() ?? string.Empty;
        var user = await userRepository.FindByEmailAsync(email, cancellationToken);
        if (user is null || string.IsNullOrEmpty(command.Password) ||
            !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(IamError.InvalidCredentials, InvalidCredentials);

        if (user.HasTwoFactor)
        {
            if (string.IsNullOrWhiteSpace(command.TwoFactorCode))
                return Result<(User user, string token)>.Failure(IamError.TwoFactorRequired, TwoFactorRequired);
            if (!hashingService.VerifyPassword(command.TwoFactorCode.Trim(), user.TwoFactorCodeHash!))
                return Result<(User user, string token)>.Failure(IamError.InvalidTwoFactorCode, InvalidTwoFactorCode);
        }

        return Result<(User user, string token)>.Success((user, tokenService.GenerateToken(user)));
    }

    /// <summary>
    ///     Registers a user with the password and the two-factor code hashed with BCrypt.
    /// </summary>
    public async Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        var invalid = Validate(command, out var role);
        if (invalid is not null) return Result<User>.Failure(IamError.InvalidUserData, invalid);

        var email = command.Email.Trim().ToLowerInvariant();
        if (await userRepository.ExistsByEmailAsync(email, cancellationToken))
            return Result<User>.Failure(IamError.EmailAlreadyTaken, "A user with the same email already exists.");

        var twoFactorCodeHash = string.IsNullOrWhiteSpace(command.TwoFactorCode)
            ? null
            : hashingService.HashPassword(command.TwoFactorCode.Trim());
        var user = new User(command, role, hashingService.HashPassword(command.Password), twoFactorCodeHash);
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<User>.Success(user);
    }

    private static string? Validate(SignUpCommand command, out Roles role)
    {
        role = Roles.RoleQaSpecialist;
        if (string.IsNullOrWhiteSpace(command.Email) || !command.Email.Contains('@'))
            return "Email is not valid.";
        if (string.IsNullOrEmpty(command.Password) || command.Password.Length < MinPasswordLength)
            return "Password must have at least 8 characters.";
        if (string.IsNullOrWhiteSpace(command.FullName)) return "Full name is required.";
        if (string.IsNullOrWhiteSpace(command.Environment) || !User.Environments.Contains(command.Environment.Trim()))
            return "Environment must be qa, production or administration.";
        if (command.OrganizationId <= 0) return "Organization id is required.";
        if (!string.IsNullOrWhiteSpace(command.Role) && !RolesExtensions.TryFromKey(command.Role, out role))
            return $"Role is not valid: {command.Role}.";
        return null;
    }
}
