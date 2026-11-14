namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Localization.Extensions;

/// <summary>
///     Extension methods to register the localized messages of the bounded contexts.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers the messages of a bounded context, so the global exception handler
    ///     can translate the message keys thrown by its value objects.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <typeparam name="TMessages">The marker class of the messages of the bounded context.</typeparam>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddBoundedContextMessages<TMessages>(this IServiceCollection services)
        where TMessages : class
    {
        return services.AddSingleton(new BoundedContextMessages(typeof(TMessages)));
    }
}
