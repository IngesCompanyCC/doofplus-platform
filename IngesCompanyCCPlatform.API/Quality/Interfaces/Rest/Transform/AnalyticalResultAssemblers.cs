using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>AnalyticalResult</c> from the resources.
/// </summary>
public static class AnalyticalResultCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a analytical result.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateAnalyticalResultCommand ToCommandFromResource(CreateAnalyticalResultResource resource)
    {
        return new CreateAnalyticalResultCommand(
            resource.BatchId,
            resource.TestName,
            resource.Parameter ?? string.Empty,
            resource.MeasuredValue,
            resource.MinimumValue,
            resource.MaximumValue,
            resource.Unit ?? string.Empty,
            resource.TestedAt);
    }
}

/// <summary>
///     Builds the resource of a analytical result.
/// </summary>
public static class AnalyticalResultResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The analytical result.</param>
    /// <returns>The resource.</returns>
    public static AnalyticalResultResource ToResourceFromEntity(AnalyticalResult entity)
    {
        return new AnalyticalResultResource(
            entity.Id.Value,
            entity.BatchId,
            entity.TestName,
            entity.Parameter,
            entity.MeasuredValue,
            entity.MinimumValue,
            entity.MaximumValue,
            entity.Unit,
            entity.Status.ToString(),
            entity.ReviewedBy,
            entity.TestedAt is null ? null : DateTime.SpecifyKind(entity.TestedAt.Value, DateTimeKind.Utc),
            entity.ReviewedAt is null ? null : DateTime.SpecifyKind(entity.ReviewedAt.Value, DateTimeKind.Utc));
    }
}
