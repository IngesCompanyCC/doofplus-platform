namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Queries;

/// <summary>
///     Query to get the organization registered with a RUC.
/// </summary>
/// <param name="Ruc">The RUC of the organization.</param>
public record GetOrganizationByRucQuery(string Ruc);
