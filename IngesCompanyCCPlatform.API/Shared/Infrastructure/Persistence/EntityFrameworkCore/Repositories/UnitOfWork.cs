using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastracture.Persistence.EntityFrameworkCore.Configuration;

namespace IngesCompanyCCPlatform.API.Shared.Infrastracture.Persistence.EntityFrameworkCore.Repositories;

public class UnitOfWork(AppDbContext context): IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}