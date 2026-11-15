namespace IngesCompanyCCPlatform.API.Iam.Interfaces.Acl;

/// <summary>
///     Lets other bounded contexts read users without knowing the IAM model.
/// </summary>
public interface IIamContextFacade
{
    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken = default);
    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken = default);
    Task<int> FetchOrganizationIdByUserId(int userId, CancellationToken cancellationToken = default);
}
