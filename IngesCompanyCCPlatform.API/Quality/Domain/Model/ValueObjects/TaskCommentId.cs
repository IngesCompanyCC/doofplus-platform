using IngesCompanyCCPlatform.API.Shared.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     The strongly typed id of <c>TaskComment</c>.
/// </summary>
/// <param name="Value">The integer value of the id.</param>
public readonly record struct TaskCommentId(int Value) : IEntityId<int>
{
    /// <summary>
    ///     Converts the id to its integer value.
    /// </summary>
    public static implicit operator int(TaskCommentId id) => id.Value;

    /// <summary>
    ///     Converts an integer to the id.
    /// </summary>
    public static implicit operator TaskCommentId(int value) => new(value);

    /// <inheritdoc />
    public override string ToString() => Value.ToString();
}
