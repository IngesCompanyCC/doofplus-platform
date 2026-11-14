namespace IngesCompanyCCPlatform.API.Iam.Domain.Model;

/// <summary>
///     Errors of the IAM bounded context. Their titles are in ErrorMessages (IamError.Case).
/// </summary>
public enum IamError
{
    None,
    InvalidCredentials,
    TwoFactorRequired,
    InvalidTwoFactorCode,
    EmailAlreadyTaken,
    UserNotFound,
    InvalidUserData,
    DatabaseError,
    InternalServerError
}
