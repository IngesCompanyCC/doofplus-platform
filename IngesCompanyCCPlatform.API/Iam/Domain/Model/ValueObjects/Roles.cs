namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;

/// <summary>
///     Roles of the users of an organization. The Web Application uses the kebab-case key
///     (<c>qa-specialist</c>); the API answers the full name (<c>ROLE_QA_SPECIALIST</c>) in the roles list.
/// </summary>
public enum Roles
{
    RoleAdministrator,
    RoleQaSpecialist,
    RoleProductionSupervisor,
    RoleQualityManager,
    RoleInternalAuditor
}

/// <summary>
///     Conversions between the role, its key (<c>qa-specialist</c>) and its name (<c>ROLE_QA_SPECIALIST</c>).
/// </summary>
public static class RolesExtensions
{
    private static readonly Dictionary<Roles, string> Keys = new()
    {
        [Roles.RoleAdministrator] = "administrator",
        [Roles.RoleQaSpecialist] = "qa-specialist",
        [Roles.RoleProductionSupervisor] = "production-supervisor",
        [Roles.RoleQualityManager] = "quality-manager",
        [Roles.RoleInternalAuditor] = "internal-auditor"
    };

    /// <summary>The kebab-case key used by the Web Application.</summary>
    public static string ToKey(this Roles role) => Keys[role];

    /// <summary>The name of the role, for example <c>ROLE_QA_SPECIALIST</c>.</summary>
    public static string ToName(this Roles role) => "ROLE_" + Keys[role].ToUpperInvariant().Replace('-', '_');

    /// <summary>
    ///     Reads a role from its key (<c>qa-specialist</c>) or its name (<c>ROLE_QA_SPECIALIST</c>).
    /// </summary>
    public static bool TryFromKey(string? value, out Roles role)
    {
        role = Roles.RoleQaSpecialist;
        if (string.IsNullOrWhiteSpace(value)) return false;
        var key = value.Trim().ToLowerInvariant().Replace('_', '-');
        if (key.StartsWith("role-")) key = key["role-".Length..];
        foreach (var (candidate, candidateKey) in Keys)
        {
            if (candidateKey != key) continue;
            role = candidate;
            return true;
        }
        return false;
    }
}
