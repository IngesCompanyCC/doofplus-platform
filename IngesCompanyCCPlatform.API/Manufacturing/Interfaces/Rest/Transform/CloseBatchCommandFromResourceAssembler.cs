using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

public static class CloseBatchCommandFromResourceAssembler
{
    public static CloseBatchCommand ToCommandFromResource(int batchId, CloseBatchResource resource)
    {
        return new CloseBatchCommand(
            batchId,
            resource.ManufacturedQuantityValue,
            resource.ManufacturedQuantityUnit);
    }
}
