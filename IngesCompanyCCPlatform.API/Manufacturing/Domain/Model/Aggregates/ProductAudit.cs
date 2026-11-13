using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;

/// <summary>
///     Audit timestamps of <see cref="Product" />, filled in by the database context.
/// </summary>
public partial class Product : IAuditableEntity
{
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }
}
