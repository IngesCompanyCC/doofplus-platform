using IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.QueryServices;

/// <summary>
///     Handles contact inquiry read use cases.
/// </summary>
public class ContactInquiryQueryService(
    IContactInquiryRepository contactInquiryRepository) : IContactInquiryQueryService
{
    public async Task<ContactInquiry?> Handle(
        GetContactInquiryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await contactInquiryRepository.FindByIdAsync(query.InquiryId, cancellationToken);
    }

    public async Task<IEnumerable<ContactInquiry>> Handle(
        GetAllContactInquiriesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await contactInquiryRepository.ListAsNoTrackingAsync(cancellationToken);
    }
}