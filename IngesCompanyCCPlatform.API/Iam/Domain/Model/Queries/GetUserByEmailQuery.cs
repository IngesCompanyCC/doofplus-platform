namespace IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;

/// <summary>
///     Query to find a user by its email address.
/// </summary>
public record GetUserByEmailQuery(string Email);
