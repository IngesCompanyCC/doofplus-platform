using IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Pipeline.Middleware.Extensions;

/// <summary>
///     Extension methods to add the shared middleware to the request pipeline.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    ///     Uses the global exception handler middleware.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The application builder.</returns>
    [Obsolete("Use app.UseExceptionHandler() with AddExceptionHandler<GlobalExceptionHandler>() instead.")]
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
#pragma warning disable CS0618 // The middleware is obsolete on purpose; this method keeps it reachable.
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
#pragma warning restore CS0618
    }
}
