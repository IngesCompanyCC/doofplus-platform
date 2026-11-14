using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="AnalyticalResult" />, filled in by the database context.
/// </summary>
public partial class AnalyticalResult : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
