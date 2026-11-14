using System.Net.Mail;
using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.CommandServices;

/// <summary>
///     Handles contact inquiry write use cases (public "Contact us" form).
/// </summary>
public class ContactInquiryCommandService(
    IContactInquiryRepository contactInquiryRepository,
    IUnitOfWork unitOfWork) : IContactInquiryCommandService
{
    public async Task<Result<ContactInquiry>> Handle(
        SendContactInquiryCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.FullName))
            return Result<ContactInquiry>.Failure(
                OrganizationsError.InvalidContactInquiryData,
                "Full name is required.");

        if (string.IsNullOrWhiteSpace(command.Email) || !IsValidEmail(command.Email))
            return Result<ContactInquiry>.Failure(
                OrganizationsError.InvalidContactInquiryData,
                "A valid email address is required.");

        if (string.IsNullOrWhiteSpace(command.Message))
            return Result<ContactInquiry>.Failure(
                OrganizationsError.InvalidContactInquiryData,
                "Message is required.");

        var inquiry = new ContactInquiry(command);

        await contactInquiryRepository.AddAsync(inquiry, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ContactInquiry>.Success(inquiry);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }
}