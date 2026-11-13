using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;

/// <summary>
///     Application service for ContactInquiry write use cases.
/// </summary>
public interface IContactInquiryCommandService
{
    Task<Result<ContactInquiry>> Handle(
        SendContactInquiryCommand command,
        CancellationToken cancellationToken = default);
}