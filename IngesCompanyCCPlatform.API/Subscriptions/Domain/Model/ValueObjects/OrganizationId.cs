using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;

/// <summary>
///     Represents the identifier of the organization that owns the subscription
///     (Organizations &amp; Profiles bounded context).
/// </summary>
public readonly record struct OrganizationId : IEntityId<int>
{
    private const string InvalidMessageKey = "subscription.error.organizationId.invalid";

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
