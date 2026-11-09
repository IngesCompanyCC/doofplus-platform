using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;

/// <summary>
///     Represents a finding or an observation recorded during an audit.
/// </summary>
public class AuditFinding
{
    private const string DescriptionNotBlankMessageKey = "audit.error.finding.description.notBlank";

    /// <summary>
    ///     Initializes a new instance of the <see cref="AuditFinding" /> entity. Used by Entity Framework Core.
    /// </summary>
    protected AuditFinding()
    {
        Description = string.Empty;
    }

    /// <param name="type">Whether it is a finding or an observation.</param>
    /// <param name="description">What the auditor found.</param>
    public AuditFinding(EFindingType type, string description)
    {
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Type = type;
        Description = description;
    }

    public int Id { get; private set; }
    public EFindingType Type { get; private set; }
    public string Description { get; private set; }
}
