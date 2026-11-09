namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the root cause of a deviation and the method used to find it, for example <c>5 Whys</c>.
/// </summary>
public record RootCause
{
    private const string MethodNotBlankMessageKey = "deviation.error.rootCause.method.notBlank";
    private const string DescriptionNotBlankMessageKey = "deviation.error.rootCause.description.notBlank";

    /// <param name="method">The analysis method, for example <c>Ishikawa</c> or <c>5 Whys</c>.</param>
    /// <param name="description">The description of the root cause.</param>
    public RootCause(string method, string description)
    {
        if (string.IsNullOrWhiteSpace(method)) throw new ArgumentException(MethodNotBlankMessageKey);
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(DescriptionNotBlankMessageKey);
        Method = method;
        Description = description;
    }

    public string Method { get; init; }
    public string Description { get; init; }
}
