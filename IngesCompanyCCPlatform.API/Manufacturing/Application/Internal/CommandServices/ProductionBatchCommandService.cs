using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Production batch command service implementation.
/// </summary>
public class ProductionBatchCommandService(
    IProductionBatchRepository productionBatchRepository,
    IUnitOfWork unitOfWork)
    : IProductionBatchCommandService
{
    /// <inheritdoc />
    public async Task<Result<ProductionBatch>> Handle(
        CreateBatchCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await productionBatchRepository.ExistsByBatchNumberAsync(command.BatchNumber, cancellationToken))
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.DuplicateBatchNumberWithCode(command.BatchNumber.Value),
                ManufacturingError.DuplicateBatchNumber);

        var batch = new ProductionBatch(command);

        await productionBatchRepository.AddAsync(batch, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ProductionBatch>.Success(batch);
    }

    /// <inheritdoc />
    public async Task<Result<ProductionBatch>> Handle(
        CloseBatchCommand command,
        CancellationToken cancellationToken = default)
    {
        var batch = await productionBatchRepository.FindByIdAsync(command.BatchId, cancellationToken);
        if (batch is null)
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.BatchNotFound,
                ManufacturingError.BatchNotFound);

        try
        {
            batch.Close(command);
        }
        catch (InvalidOperationException)
        {
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.InvalidBatchState,
                ManufacturingError.InvalidBatchState);
        }

        productionBatchRepository.Update(batch);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ProductionBatch>.Success(batch);
    }

    /// <inheritdoc />
    public async Task<Result<Incident>> Handle(
        RegisterIncidentCommand command,
        CancellationToken cancellationToken = default)
    {
        var batch = await productionBatchRepository.FindByIdAsync(command.BatchId, cancellationToken);
        if (batch is null)
            return Result<Incident>.Failure(
                ManufacturingErrors.BatchNotFound,
                ManufacturingError.BatchNotFound);

        var incident = batch.RegisterIncident(command);

        productionBatchRepository.Update(batch);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Incident>.Success(incident);
    }

    public async Task<Result<ProductionBatch>> Handle(
        UpdateBatchProgressCommand command,
        CancellationToken cancellationToken = default)
    {
        var batch = await productionBatchRepository.FindByIdAsync(command.BatchId, cancellationToken);
        if (batch is null)
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.BatchNotFound,
                ManufacturingError.BatchNotFound);

        batch.UpdateProgress(command.Progress);

        productionBatchRepository.Update(batch);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ProductionBatch>.Success(batch);
    }

    public Task<Result<ProductionBatch>> Handle(StartBatchCommand command, CancellationToken cancellationToken = default)
        => ChangeStatusAsync(command.BatchId, batch => batch.Start(), cancellationToken);

    public Task<Result<ProductionBatch>> Handle(ResumeBatchCommand command, CancellationToken cancellationToken = default)
        => ChangeStatusAsync(command.BatchId, batch => batch.Resume(), cancellationToken);

    public Task<Result<ProductionBatch>> Handle(ReleaseBatchCommand command, CancellationToken cancellationToken = default)
        => ChangeStatusAsync(command.BatchId, batch => batch.Release(command.ReleasedBy), cancellationToken);

    private async Task<Result<ProductionBatch>> ChangeStatusAsync(
        int batchId,
        Action<ProductionBatch> change,
        CancellationToken cancellationToken)
    {
        var batch = await productionBatchRepository.FindByIdAsync(batchId, cancellationToken);
        if (batch is null)
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.BatchNotFound,
                ManufacturingError.BatchNotFound);

        try
        {
            change(batch);
        }
        catch (InvalidOperationException)
        {
            return Result<ProductionBatch>.Failure(
                ManufacturingErrors.InvalidBatchState,
                ManufacturingError.InvalidBatchState);
        }

        productionBatchRepository.Update(batch);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<ProductionBatch>.Success(batch);
    }
}
