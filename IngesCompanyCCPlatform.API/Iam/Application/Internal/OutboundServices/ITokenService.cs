using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;

namespace IngesCompanyCCPlatform.API.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}
