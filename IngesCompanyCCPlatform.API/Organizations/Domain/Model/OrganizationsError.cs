namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model;

/// <summary>
///     Enumeration of domain errors for the Organizations bounded context.
/// </summary>
public enum OrganizationsError
{
    None,
    OrganizationNotFound,
    DuplicateRuc,
    OrganizationAlreadyActive,
    OrganizationSuspended,
    UserProfileNotFound,
    DuplicateUserEmail,
    FacilityNotFound,
    ContactInquiryNotFound,
    InvalidContactInquiryData,
    InvalidOperation,
    DatabaseError,
    InternalServerError
}