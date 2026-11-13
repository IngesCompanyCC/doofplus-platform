using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="Alert" />, filled in by the database context.
/// </summary>
public partial class Alert : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
