using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Deviation</c> from the resources.
/// </summary>
public static class DeviationCommandFromResourceAssembler
{
    private const string SeverityInvalidMessageKey = "quality.deviation.error.severity.invalid";

    /// <summary>
    ///     Builds the command to create a deviation.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateDeviationCommand ToCommandFromResource(CreateDeviationResource resource)
    {
        return new CreateDeviationCommand(
            resource.Code,
            resource.Title,
            resource.Description,
            EnumParser.Parse<EDeviationSeverity>(resource.Severity, SeverityInvalidMessageKey),
            resource.BatchId,
            resource.AssignedTo);
    }
}

/// <summary>
///     Builds the resource of a deviation.
/// </summary>
public static class DeviationResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The deviation.</param>
    /// <returns>The resource.</returns>
    public static DeviationResource ToResourceFromEntity(Deviation entity)
    {
        return new DeviationResource(
            entity.Id.Value,
            entity.Code,
            entity.Title,
            entity.Description,
            entity.Severity.ToString(),
            entity.BatchId,
            entity.AssignedTo,
            entity.Status.ToString(),
            DateTime.SpecifyKind(entity.DetectedAt, DateTimeKind.Utc),
            entity.ClosedAt is null ? null : DateTime.SpecifyKind(entity.ClosedAt.Value, DateTimeKind.Utc));
    }
}
