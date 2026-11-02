namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public sealed record SensorCode
{
    public SensorCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("SensorCode cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}