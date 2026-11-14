using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="QualityDocument" />, filled in by the database context.
/// </summary>
public partial class QualityDocument : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
