namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public sealed recprd Facility
{
    public Facility(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Facility cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}