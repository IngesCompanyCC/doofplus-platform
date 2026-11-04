namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
///     Role of a user inside an organization (EP02).
/// </summary>
public enum UserRole
{
    Admin = 0,
    QaSpecialist = 1,
    ProductionSupervisor = 2
}