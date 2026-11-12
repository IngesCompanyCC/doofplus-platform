using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Transform;

public static class RegisterIncidentCommandFromResourceAssembler
{
    public static RegisterIncidentCommand ToCommandFromResource(int batchId, RegisterIncidentResource resource)
    {
        return new RegisterIncidentCommand(
            batchId,
            resource.Description,
            resource.Severity);
    }
}
