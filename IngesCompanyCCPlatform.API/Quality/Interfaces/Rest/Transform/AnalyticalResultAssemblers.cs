using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>AnalyticalResult</c> from the resources. Enums are accepted in any case and with hyphens.
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
            resource.BatchCode ?? string.Empty,
            resource.SampleCode ?? string.Empty,
            resource.ReportCode ?? string.Empty,
            resource.Test ?? string.Empty,
            resource.Protocol ?? string.Empty,
            resource.Result ?? string.Empty,
            resource.Specification ?? string.Empty,
            resource.Analyst ?? string.Empty,
            resource.Status is null ? EResultStatus.Pending : EnumParser.Parse<EResultStatus>(resource.Status, "quality.analyticalResult.error.status.invalid"),
            resource.Approval is null ? EApprovalState.Requested : EnumParser.Parse<EApprovalState>(resource.Approval, "quality.analyticalResult.error.approval.invalid"));
    }

    /// <summary>
    ///     Builds the command to replace a analytical result.
    /// </summary>
    /// <param name="analyticalResultId">The id of the analytical result.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateAnalyticalResultCommand ToCommandFromResource(int analyticalResultId, UpdateAnalyticalResultResource resource)
    {
        return new UpdateAnalyticalResultCommand(
            analyticalResultId,
            resource.BatchCode ?? string.Empty,
            resource.SampleCode ?? string.Empty,
            resource.ReportCode ?? string.Empty,
            resource.Test ?? string.Empty,
            resource.Protocol ?? string.Empty,
            resource.Result ?? string.Empty,
            resource.Specification ?? string.Empty,
            resource.Analyst ?? string.Empty,
            resource.Status is null ? EResultStatus.Pending : EnumParser.Parse<EResultStatus>(resource.Status, "quality.analyticalResult.error.status.invalid"),
            resource.Approval is null ? EApprovalState.Requested : EnumParser.Parse<EApprovalState>(resource.Approval, "quality.analyticalResult.error.approval.invalid"));
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
            entity.BatchCode,
            entity.SampleCode,
            entity.ReportCode,
            entity.Test,
            entity.Protocol,
            entity.Result,
            entity.Specification,
            entity.Analyst,
            entity.Status.ToString(),
            entity.Approval.ToString());
    }
}
