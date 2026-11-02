namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

sealed record AlertCode
{
    public AlertCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("AlertCode cannot be null or empty.", nameof(value));
        }

        Value = value;
    }
    public string Value { get; }

    public override string ToString() => Value;
}