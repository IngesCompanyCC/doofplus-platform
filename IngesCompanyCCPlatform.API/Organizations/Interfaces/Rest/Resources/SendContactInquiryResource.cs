namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record SendContactInquiryResource(
    string FullName,
    string Email,
    string Message);