namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public sealed record CalibrationStatus
{
    privvate const int MaxLength = 255;

    public CalibrationStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("CalibrationStatus cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}