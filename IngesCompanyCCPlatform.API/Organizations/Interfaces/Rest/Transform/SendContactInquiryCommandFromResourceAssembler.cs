using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class SendContactInquiryCommandFromResourceAssembler
{
    public static SendContactInquiryCommand ToCommandFromResource(
        SendContactInquiryResource resource)
    {
        return new SendContactInquiryCommand(
            resource.FullName,
            resource.Email,
            resource.Message);
    }
}