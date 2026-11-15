using System.Text.Json.Serialization;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;

/// <summary>
///     A user of an organization. The password and the two-factor code are stored hashed with BCrypt.
/// </summary>
public partial class User
{
    public static readonly string[] Environments = ["qa", "production", "administration"];

    private User()
    {
    }

    /// <summary>
    ///     Creates the user of a sign-up. The command must be valid; the password and the code arrive hashed.
    /// </summary>
    public User(SignUpCommand command, Roles role, string passwordHash, string? twoFactorCodeHash)
    {
        Email = command.Email.Trim().ToLowerInvariant();
        PasswordHash = passwordHash;
        FullName = command.FullName.Trim();
        Role = role;
        Environment = command.Environment.Trim();
        Facility = command.Facility?.Trim() ?? string.Empty;
        Status = string.IsNullOrWhiteSpace(command.Status) ? "active" : command.Status.Trim();
        Privilege = string.IsNullOrWhiteSpace(command.Privilege) ? "none" : command.Privilege.Trim();
        OrganizationId = command.OrganizationId;
        OrganizationName = command.OrganizationName?.Trim() ?? string.Empty;
        Plant = command.Plant?.Trim() ?? string.Empty;
        TwoFactorCodeHash = twoFactorCodeHash;
    }

    public int Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    [JsonIgnore] public string PasswordHash { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public Roles Role { get; private set; }
    public string Environment { get; private set; } = string.Empty;
    public string Facility { get; private set; } = string.Empty;
    public string Status { get; private set; } = "active";
    public string Privilege { get; private set; } = "none";
    public int OrganizationId { get; private set; }
    public string OrganizationName { get; private set; } = string.Empty;
    public string Plant { get; private set; } = string.Empty;
    [JsonIgnore] public string? TwoFactorCodeHash { get; private set; }

    /// <summary>True when the user enrolled two-factor authentication.</summary>
    public bool HasTwoFactor => !string.IsNullOrWhiteSpace(TwoFactorCodeHash);

    /// <summary>Initials of the full name, for example EP for Elena Paz.</summary>
    public string Initials => string.Concat(FullName
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Take(2)
        .Select(part => char.ToUpperInvariant(part[0])));
}
