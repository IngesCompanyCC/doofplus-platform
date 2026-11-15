using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>TraceabilityGap</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class TraceabilityGapCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a traceability gap.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateTraceabilityGapCommand ToCommandFromResource(CreateTraceabilityGapResource resource)
    {
        return new CreateTraceabilityGapCommand(
            resource.BatchCode ?? string.Empty,
            resource.Product ?? string.Empty,
            resource.MissingRecord ?? string.Empty,
            resource.Detail ?? string.Empty,
            resource.Status is null ? EGapStatus.Open : EnumParser.Parse<EGapStatus>(resource.Status, "quality.traceabilityGap.error.status.invalid"));
    }
}

/// <summary>
///     Builds the resource of a traceability gap.
/// </summary>
public static class TraceabilityGapResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The traceability gap.</param>
    /// <returns>The resource.</returns>
    public static TraceabilityGapResource ToResourceFromEntity(TraceabilityGap entity)
    {
        return new TraceabilityGapResource(
            entity.Id.Value,
            entity.BatchCode,
            entity.Product,
            entity.MissingRecord,
            entity.Detail,
            entity.Status.ToString());
    }
}
