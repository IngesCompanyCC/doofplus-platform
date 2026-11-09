using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a deviation in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct DeviationId(int Value) : IEntityId<int>
{
    public static implicit operator int(DeviationId id) => id.Value;
    public static implicit operator DeviationId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
