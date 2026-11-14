using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>TraceabilityGap</c> from the resources.
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
            resource.BatchId,
            resource.Description,
            resource.MissingReference ?? string.Empty,
            resource.AffectedProcess ?? string.Empty);
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
            entity.BatchId,
            entity.Description,
            entity.MissingReference,
            entity.AffectedProcess,
            entity.Status.ToString(),
            entity.ReviewedBy,
            DateTime.SpecifyKind(entity.DetectedAt, DateTimeKind.Utc),
            entity.ResolvedAt is null ? null : DateTime.SpecifyKind(entity.ResolvedAt.Value, DateTimeKind.Utc));
    }
}
