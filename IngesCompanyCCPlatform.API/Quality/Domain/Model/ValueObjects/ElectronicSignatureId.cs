using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of a electronic signature that lives in the Identity &amp; Access Management bounded context.
/// </summary>
/// <remarks>
///     Quality &amp; Compliance only keeps the identifier: the reference between bounded contexts is logical.
/// </remarks>
public readonly record struct ElectronicSignatureId : IEntityId<int>
{
    private const string InvalidMessageKey = "quality.error.signatureId.invalid";

    /// <param name="value">
    ///     The positive identifier of the electronic signature.
    /// </param>
    public ElectronicSignatureId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }

    /// <inheritdoc />
    public int Value { get; }

    public static implicit operator int(ElectronicSignatureId id) => id.Value;
    public static implicit operator ElectronicSignatureId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
