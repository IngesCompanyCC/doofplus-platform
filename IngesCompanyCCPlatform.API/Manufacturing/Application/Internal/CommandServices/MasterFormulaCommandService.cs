using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Command service of the master formulas.
/// </summary>
public class MasterFormulaCommandService(
    IMasterFormulaRepository masterFormulaRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IMasterFormulaCommandService
{
    /// <inheritdoc />
    public async Task<Result<MasterFormula>> Handle(CreateMasterFormulaCommand command,
        CancellationToken cancellationToken)
    {
        if (!await productRepository.ExistsByCodeAsync(command.ProductCode, cancellationToken))
            return Result<MasterFormula>.Failure(ProductErrors.NotFoundByCode(command.ProductCode.Value),
                ManufacturingError.ProductNotFound);
        if (await masterFormulaRepository.ExistsByProductCodeAndVersionAsync(command.ProductCode,
                command.Version.Trim(), cancellationToken))
            return Result<MasterFormula>.Failure(MasterFormulaErrors.DuplicateVersion,
                ManufacturingError.DuplicateFormulaVersion);

        var formula = new MasterFormula(command);
        await masterFormulaRepository.AddAsync(formula, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<MasterFormula>.Success(formula);
    }

    /// <inheritdoc />
    public async Task<Result<MasterFormula>> Handle(ApproveMasterFormulaCommand command,
        CancellationToken cancellationToken)
    {
        var formula = await masterFormulaRepository.FindByIdAsync(command.MasterFormulaId, cancellationToken);
        if (formula is null)
            return Result<MasterFormula>.Failure(MasterFormulaErrors.NotFoundById(command.MasterFormulaId),
                ManufacturingError.MasterFormulaNotFound);

        try
        {
            formula.Approve(command.ApprovedBy);
        }
        catch (InvalidOperationException exception)
        {
            return Result<MasterFormula>.Failure(MasterFormulaErrors.InvalidState(exception.Message),
                ManufacturingError.InvalidFormulaState);
        }

        // Only one approved version per product: the one approved before becomes obsolete
        var formulasOfProduct = await masterFormulaRepository.FindAllByProductCodeAsync(formula.ProductCode,
            cancellationToken);
        foreach (var other in formulasOfProduct.Where(other => other.Id != formula.Id)) other.Lock();

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<MasterFormula>.Success(formula);
    }
}
