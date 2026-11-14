namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     How serious an audit finding is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EAuditFindingSeverity
{
    Low,
    Medium,
    High,
    Critical
}
