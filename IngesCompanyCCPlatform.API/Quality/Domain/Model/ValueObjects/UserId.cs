using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of a user that lives in the Identity &amp; Access Management bounded context.
/// </summary>
/// <remarks>
///     Quality &amp; Compliance only keeps the identifier: the reference between bounded contexts is logical.
/// </remarks>
public readonly record struct UserId : IEntityId<int>
{
    private const string InvalidMessageKey = "quality.error.userId.invalid";

    /// <param name="value">
    ///     The positive identifier of the user.
    /// </param>
    public UserId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }

    /// <inheritdoc />
    public int Value { get; }

    public static implicit operator int(UserId id) => id.Value;
    public static implicit operator UserId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
