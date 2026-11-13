using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     Aggregate root that represents a contact inquiry submitted from the public landing page.
/// </summary>
public class ContactInquiry : IAuditableEntity
{
    public int Id { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public InquiryStatus Status { get; private set; }
    public DateTimeOffset ReceivedAt { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // Required by EF Core
    private ContactInquiry()
    {
    }

    public ContactInquiry(SendContactInquiryCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        FullName = command.FullName.Trim();
        Email = command.Email.Trim().ToLowerInvariant();
        Message = command.Message.Trim();
        Status = InquiryStatus.Received;
        ReceivedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    ///     Marks the inquiry as answered by the support team.
    /// </summary>
    public void MarkAsAnswered()
    {
        if (Status == InquiryStatus.Answered)
            return;

        Status = InquiryStatus.Answered;
    }
}