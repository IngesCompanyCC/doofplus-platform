namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to register a new contact inquiry from the public form.
/// </summary>
public record SendContactInquiryCommand(string FullName, string Email, string Message);