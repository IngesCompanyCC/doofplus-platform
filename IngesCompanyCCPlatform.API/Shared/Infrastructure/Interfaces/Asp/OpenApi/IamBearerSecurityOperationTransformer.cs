using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.OpenApi;

/// <summary>
///     Marks with the Bearer scheme the operations that use the IAM [Authorize] attribute.
/// </summary>
public sealed class IamBearerSecurityOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var metadata = context.Description.ActionDescriptor.EndpointMetadata;
        if (metadata.OfType<AllowAnonymousAttribute>().Any() || !metadata.OfType<AuthorizeAttribute>().Any())
            return Task.CompletedTask;
        operation.Security ??= [];
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", context.Document)] = []
        });
        return Task.CompletedTask;
    }
}
