namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public sealed record DeviceId
{
    public DeviceId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("DeviceId cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}