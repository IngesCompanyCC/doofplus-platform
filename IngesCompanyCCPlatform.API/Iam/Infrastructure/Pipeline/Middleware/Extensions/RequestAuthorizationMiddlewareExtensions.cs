using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
