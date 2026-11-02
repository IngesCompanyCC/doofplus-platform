namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public sealed reecord EquipmentCode
{
    public EquipmentCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("EquipmentCode cannot be null or empty.", nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}