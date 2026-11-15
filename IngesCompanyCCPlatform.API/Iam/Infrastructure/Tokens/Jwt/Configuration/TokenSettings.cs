namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Tokens.Jwt.Configuration;

/// <summary>
///     Settings of the JWT (section TokenSettings; in production set TokenSettings__Secret).
/// </summary>
public class TokenSettings
{
    public required string Secret { get; set; }
    public int ExpirationDays { get; set; } = 7;
}
