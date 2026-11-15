using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Deviation</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class DeviationCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a deviation.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateDeviationCommand ToCommandFromResource(CreateDeviationResource resource)
    {
        return new CreateDeviationCommand(
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.BatchCode ?? string.Empty,
            resource.OrderCode ?? string.Empty,
            resource.Summary ?? string.Empty,
            resource.Classification is null ? EClassification.Minor : EnumParser.Parse<EClassification>(resource.Classification, "quality.deviation.error.classification.invalid"),
            resource.Containment ?? string.Empty,
            resource.Severity ?? 1,
            resource.Likelihood ?? 1,
            resource.Detectability ?? 1,
            resource.Status is null ? EDeviationStatus.Open : EnumParser.Parse<EDeviationStatus>(resource.Status, "quality.deviation.error.status.invalid"),
            resource.RootCause ?? string.Empty,
            resource.CapaCode ?? string.Empty,
            resource.ReportedBy ?? string.Empty,
            resource.ReportedAt,
            resource.Owner ?? string.Empty,
            resource.DueAt);
    }

    /// <summary>
    ///     Builds the command to replace a deviation.
    /// </summary>
    /// <param name="deviationId">The id of the deviation.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateDeviationCommand ToCommandFromResource(int deviationId, UpdateDeviationResource resource)
    {
        return new UpdateDeviationCommand(
            deviationId,
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.BatchCode ?? string.Empty,
            resource.OrderCode ?? string.Empty,
            resource.Summary ?? string.Empty,
            resource.Classification is null ? EClassification.Minor : EnumParser.Parse<EClassification>(resource.Classification, "quality.deviation.error.classification.invalid"),
            resource.Containment ?? string.Empty,
            resource.Severity ?? 1,
            resource.Likelihood ?? 1,
            resource.Detectability ?? 1,
            resource.Status is null ? EDeviationStatus.Open : EnumParser.Parse<EDeviationStatus>(resource.Status, "quality.deviation.error.status.invalid"),
            resource.RootCause ?? string.Empty,
            resource.CapaCode ?? string.Empty,
            resource.ReportedBy ?? string.Empty,
            resource.ReportedAt,
            resource.Owner ?? string.Empty,
            resource.DueAt);
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
            entity.BatchCode,
            entity.OrderCode,
            entity.Summary,
            entity.Classification.ToString(),
            entity.Containment,
            entity.Severity,
            entity.Likelihood,
            entity.Detectability,
            entity.Status.ToString(),
            entity.RootCause,
            entity.CapaCode,
            entity.ReportedBy,
            entity.ReportedAt is null ? null : DateTime.SpecifyKind(entity.ReportedAt.Value, DateTimeKind.Utc),
            entity.Owner,
            entity.DueAt is null ? null : DateTime.SpecifyKind(entity.DueAt.Value, DateTimeKind.Utc));
    }
}
