using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class ContactInquiryResourceFromEntityAssembler
{
    public static ContactInquiryResource ToResourceFromEntity(ContactInquiry entity)
    {
        return new ContactInquiryResource(
            entity.Id,
            entity.FullName,
            entity.Email,
            entity.Message,
            entity.Status.ToString(),
            entity.ReceivedAt,
            entity.CreatedAt);
    }
}