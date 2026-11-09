using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Turns the rules broken inside the aggregates of Quality &amp; Compliance into failed results.
/// </summary>
/// <remarks>
///     The aggregates throw <see cref="ArgumentException" /> for invalid data and
///     <see cref="InvalidOperationException" /> for an action that their current status does not allow.
/// </remarks>
internal static class QualityDomainRules
{
    /// <summary>
    ///     Creates an aggregate and returns a failure when its data is invalid.
    /// </summary>
    /// <param name="factory">Creates the aggregate.</param>
    /// <typeparam name="T">The type of the aggregate.</typeparam>
    /// <returns>The created aggregate, or the broken rule.</returns>
    public static Result<T> Create<T>(Func<T> factory)
    {
        return Apply(default(T)!, _ => { }, factory);
    }

    /// <summary>
    ///     Runs a behavior of an aggregate and returns a failure when it breaks a rule.
    /// </summary>
    /// <param name="aggregate">The aggregate.</param>
    /// <param name="behavior">The behavior to run.</param>
    /// <typeparam name="T">The type of the aggregate.</typeparam>
    /// <returns>The aggregate, or the broken rule.</returns>
    public static Result<T> Apply<T>(T aggregate, Action<T> behavior)
    {
        return Apply(aggregate, behavior, null);
    }

    private static Result<T> Apply<T>(T aggregate, Action<T> behavior, Func<T>? factory)
    {
        try
        {
            if (factory is not null) aggregate = factory();
            behavior(aggregate);
            return Result<T>.Success(aggregate);
        }
        catch (InvalidOperationException exception)
        {
            return Result<T>.Failure(QualityErrors.InvalidState(exception.Message), QualityError.InvalidState);
        }
        catch (ArgumentException exception)
        {
            return Result<T>.Failure(QualityErrors.InvalidData(exception.Message), QualityError.InvalidData);
        }
    }
}
