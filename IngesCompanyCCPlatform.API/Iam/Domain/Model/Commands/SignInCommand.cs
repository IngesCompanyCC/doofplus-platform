namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;

/// <summary>
///     Command to sign in. The two-factor code is empty in the first step.
/// </summary>
public record SignInCommand(string Email, string Password, string? TwoFactorCode);
