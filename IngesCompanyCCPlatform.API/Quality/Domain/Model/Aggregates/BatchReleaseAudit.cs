using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="BatchRelease" />, filled in by the database context.
/// </summary>
public partial class BatchRelease : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
