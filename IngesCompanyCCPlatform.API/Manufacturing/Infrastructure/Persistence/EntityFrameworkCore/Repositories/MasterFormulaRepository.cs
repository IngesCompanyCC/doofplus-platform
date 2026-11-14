using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="MasterFormula" />.
/// </summary>
public class MasterFormulaRepository(AppDbContext context) : BaseRepository<MasterFormula>(context), IMasterFormulaRepository
{
    /// <inheritdoc />
    public override async Task<MasterFormula?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var formulaId = new MasterFormulaId(id);
        return await Context.Set<MasterFormula>().Include(formula => formula.Items).FirstOrDefaultAsync(formula => formula.Id == formulaId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<MasterFormula?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var formulaId = new MasterFormulaId(id);
        return await Context.Set<MasterFormula>().Include(formula => formula.Items).AsNoTracking().FirstOrDefaultAsync(formula => formula.Id == formulaId, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<IEnumerable<MasterFormula>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<MasterFormula>().Include(formula => formula.Items).AsNoTracking().ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByProductCodeAndVersionAsync(ProductCode productCode, string version,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<MasterFormula>()
            .AnyAsync(formula => formula.ProductCode == productCode && formula.Version == version, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsync(ProductCode productCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<MasterFormula>()
            .Where(formula => formula.ProductCode == productCode)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsNoTrackingAsync(ProductCode productCode,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<MasterFormula>()
            .Include(formula => formula.Items)
            .AsNoTracking()
            .Where(formula => formula.ProductCode == productCode)
            .ToListAsync(cancellationToken);
    }
}
