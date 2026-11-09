using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts an <see cref="AnalyticalResult" /> aggregate into an <see cref="AnalyticalResultResource" />.
/// </summary>
public static class AnalyticalResultResourceFromEntityAssembler
{
    public static AnalyticalResultResource ToResourceFromEntity(AnalyticalResult entity)
    {
        return new AnalyticalResultResource(entity.Id, entity.ProductionBatchId, entity.TestName, entity.Value,
            entity.Specification.Min, entity.Specification.Max, entity.Specification.Unit,
            entity.OutOfSpecification, entity.AnalystId);
    }
}

/// <summary>
///     Converts a <see cref="RegisterAnalyticalResultResource" /> into a <see cref="RegisterAnalyticalResultCommand" />.
/// </summary>
public static class RegisterAnalyticalResultCommandFromResourceAssembler
{
    public static RegisterAnalyticalResultCommand ToCommandFromResource(RegisterAnalyticalResultResource resource)
    {
        return new RegisterAnalyticalResultCommand(
            new ProductionBatchId(resource.ProductionBatchId),
            resource.TestName,
            resource.Value,
            new SpecificationRange(resource.SpecMin, resource.SpecMax, resource.Unit),
            new UserId(resource.AnalystId));
    }
}
