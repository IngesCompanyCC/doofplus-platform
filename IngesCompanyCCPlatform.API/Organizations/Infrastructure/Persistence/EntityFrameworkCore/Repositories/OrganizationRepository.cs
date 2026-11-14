using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class OrganizationRepository(AppDbContext context)
    : BaseRepository<Organization>(context), IOrganizationRepository
{
    public async Task<Organization?> FindByRucAsync(
        string ruc,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Organization>()
            .FirstOrDefaultAsync(organization => organization.Ruc == ruc, cancellationToken);
    }

    public async Task<bool> ExistsByRucAsync(
        string ruc,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Organization>()
            .AnyAsync(organization => organization.Ruc == ruc, cancellationToken);
    }
}