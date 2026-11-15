namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Credentials to sign in. The two-factor code goes in the second step, after the 401 two-factor-required.
/// </summary>
public record SignInResource(string Email, string Password, string? TwoFactorCode);
