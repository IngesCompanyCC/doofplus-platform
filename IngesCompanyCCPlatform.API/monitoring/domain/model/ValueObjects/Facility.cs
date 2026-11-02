using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public partial record FacilityId{
    private const int MaxLength = 128;
    private const string NotBlankMessageKey = "monitoring.device.error.id.notBlank";
    private const string SizeMessageKey = "monitoring.device.error.id.size";
    public string Value { get; }

    public FacilityId(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException(NotBlankMessageKey); }

        if (value.Length > MaxLength) { throw new ArgumentException(SizeMessageKey); }

        Value = value;
    }

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]

    private static partial Regex AllowedPattern();

}