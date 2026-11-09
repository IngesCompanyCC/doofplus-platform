using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a quality document in the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct QualityDocumentId(int Value) : IEntityId<int>
{
    public static implicit operator int(QualityDocumentId id) => id.Value;
    public static implicit operator QualityDocumentId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
