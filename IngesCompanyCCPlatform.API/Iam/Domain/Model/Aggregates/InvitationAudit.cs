using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="Invitation" />, filled in by the database context.
/// </summary>
public partial class Invitation : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
