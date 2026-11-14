using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.OpenApi;

/// <summary>
///     Adds the Bearer (JWT) security scheme to the OpenAPI document when JWT Bearer authentication
///     is configured, so the endpoints can be tried with a token from the API reference.
/// </summary>
/// <param name="authenticationSchemeProvider">The provider of the configured authentication schemes.</param>
public sealed class BearerSecuritySchemeTransformer
    : IOpenApiDocumentTransformer
{
    /// <inheritdoc />
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                In = ParameterLocation.Header,
                BearerFormat = "JWT",
                Description = "Enter JWT Bearer token"
            }
        };
        return Task.CompletedTask;
    }
}
