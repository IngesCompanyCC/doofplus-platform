using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of a organization that lives in the Organizations &amp; Profiles bounded context.
/// </summary>
/// <remarks>
///     Quality &amp; Compliance only keeps the identifier: the reference between bounded contexts is logical.
/// </remarks>
public readonly record struct OrganizationId : IEntityId<int>
{
    private const string InvalidMessageKey = "quality.error.organizationId.invalid";

    /// <param name="value">
    ///     The positive identifier of the organization.
    /// </param>
    public OrganizationId(int value)
    {
        if (value <= 0) throw new ArgumentException(InvalidMessageKey);
        Value = value;
    }

    /// <inheritdoc />
    public int Value { get; }

    public static implicit operator int(OrganizationId id) => id.Value;
    public static implicit operator OrganizationId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
