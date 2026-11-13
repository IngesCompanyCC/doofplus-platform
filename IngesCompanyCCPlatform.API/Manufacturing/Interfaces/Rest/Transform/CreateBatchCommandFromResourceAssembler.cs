using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

public static class CreateBatchCommandFromResourceAssembler
{
    public static CreateBatchCommand ToCommandFromResource(CreateBatchResource resource)
    {
        return new CreateBatchCommand(
            resource.BatchNumber,
            resource.ProductionOrderId,
            resource.PlannedQuantityValue,
            resource.PlannedQuantityUnit,
            resource.Line,
            resource.Owner);
    }
}
