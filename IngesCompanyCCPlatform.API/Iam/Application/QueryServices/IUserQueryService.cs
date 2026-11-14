using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Iam.Application.QueryServices;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default);
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default);
    Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken = default);
}
