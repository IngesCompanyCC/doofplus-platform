namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;



/// /// <summary>
///     Command to register a laboratory organization, its primary facility
///     and the first administrator.
/// </summary>
public record RegisterOrganizationCommand(
    string LegalName,
    string Ruc,
    string? Region,
    string OwnerName);