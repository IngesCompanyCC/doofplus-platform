using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a material approval in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct MaterialApprovalId(int Value) : IEntityId<int>
{
    public static implicit operator int(MaterialApprovalId id) => id.Value;
    public static implicit operator MaterialApprovalId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
