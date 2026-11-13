namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record ContactInquiryResource(
    int Id,
    string FullName,
    string Email,
    string Message,
    string Status,
    DateTimeOffset ReceivedAt,
    DateTimeOffset? CreatedAt);