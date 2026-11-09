using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of a raw material lot that lives in the Manufacturing &amp; Batch Management bounded context.
/// </summary>
/// <remarks>
///     Quality &amp; Compliance only keeps the identifier: the reference between bounded contexts is logical.
/// </remarks>
public readonly record struct RawMaterialLotId : IEntityId<int>
{
    private const string InvalidMessageKey = "quality.error.rawMaterialLotId.invalid";

    /// <param name="value">
    ///     The positive identifier of the raw material lot.
    /// </param>
    public RawMaterialLotId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }

    /// <inheritdoc />
    public int Value { get; }

    public static implicit operator int(RawMaterialLotId id) => id.Value;
    public static implicit operator RawMaterialLotId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
