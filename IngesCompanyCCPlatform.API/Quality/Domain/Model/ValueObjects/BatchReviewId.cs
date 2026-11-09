using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a batch review in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct BatchReviewId(int Value) : IEntityId<int>
{
    public static implicit operator int(BatchReviewId id) => id.Value;
    public static implicit operator BatchReviewId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
