using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of a production batch that lives in the Manufacturing &amp; Batch Management bounded context.
/// </summary>
/// <remarks>
///     Quality &amp; Compliance only keeps the identifier: the reference between bounded contexts is logical.
/// </remarks>
public readonly record struct ProductionBatchId : IEntityId<int>
{
    private const string InvalidMessageKey = "quality.error.productionBatchId.invalid";

    /// <param name="value">
    ///     The positive identifier of the production batch.
    /// </param>
    public ProductionBatchId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }

    /// <inheritdoc />
    public int Value { get; }

    public static implicit operator int(ProductionBatchId id) => id.Value;
    public static implicit operator ProductionBatchId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
