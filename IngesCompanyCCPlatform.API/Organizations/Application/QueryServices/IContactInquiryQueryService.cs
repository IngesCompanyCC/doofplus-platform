using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Organizations.Application.QueryServices;

/// <summary>
///     Application service for ContactInquiry read use cases.
/// </summary>
public interface IContactInquiryQueryService
{
    Task<ContactInquiry?> Handle(
        GetContactInquiryByIdQuery query,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ContactInquiry>> Handle(
        GetAllContactInquiriesQuery query,
        CancellationToken cancellationToken = default);
}