namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
///     Peruvian RUC (Registro Único de Contribuyentes). Must be exactly 11 digits.
/// </summary>
public record Ruc
{
    public string Value { get; }

    public Ruc(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("RUC cannot be empty.", nameof(value));

        var cleaned = value.Trim();
        if (cleaned.Length != 11 || !cleaned.All(char.IsDigit))
            throw new ArgumentException("RUC must be exactly 11 digits.", nameof(value));

        Value = cleaned;
    }

    public override string ToString() => Value;
}   