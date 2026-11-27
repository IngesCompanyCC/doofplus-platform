using System.Reflection;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Interfaces.Asp.OpenApi;

/// <summary>
///     Sets the version of the OpenAPI document to the version of the API assembly,
///     so the published reference shows the released version instead of the default one.
/// </summary>
public sealed class ApiVersionDocumentTransformer : IOpenApiDocumentTransformer
{
    private static readonly string ApiVersion = ResolveApiVersion();

    /// <inheritdoc />
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info ??= new OpenApiInfo();
        document.Info.Version = ApiVersion;
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Reads the informational version of the assembly (the project Version) without the source revision suffix.
    /// </summary>
    private static string ResolveApiVersion()
    {
        var assembly = typeof(ApiVersionDocumentTransformer).Assembly;
        var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                      ?? assembly.GetName().Version?.ToString(3)
                      ?? "1.0.0";
        var metadataStart = version.IndexOf('+');
        return metadataStart >= 0 ? version[..metadataStart] : version;
    }
}
