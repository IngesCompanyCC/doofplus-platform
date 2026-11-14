using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class FacilityRepository(AppDbContext context)
    : BaseRepository<Facility>(context), IFacilityRepository
{
    public async Task<IEnumerable<Facility>> FindByOrganizationIdAsync(
        int organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Facility>()
            .Where(facility => facility.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByOrganizationIdAndNameAsync(
        int organizationId,
        string name,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Facility>()
            .AnyAsync(facility => facility.OrganizationId == organizationId && facility.Name == name, cancellationToken);
    }
}
