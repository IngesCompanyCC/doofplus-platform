using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;

/// <summary>
///     Repository contract for the ContactInquiry aggregate root.
/// </summary>
public interface IContactInquiryRepository : IBaseRepository<ContactInquiry>
{ }