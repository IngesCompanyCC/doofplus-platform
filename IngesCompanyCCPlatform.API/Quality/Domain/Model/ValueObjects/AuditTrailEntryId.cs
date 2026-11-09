using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a audit trail entry in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct AuditTrailEntryId(int Value) : IEntityId<int>
{
    public static implicit operator int(AuditTrailEntryId id) => id.Value;
    public static implicit operator AuditTrailEntryId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
