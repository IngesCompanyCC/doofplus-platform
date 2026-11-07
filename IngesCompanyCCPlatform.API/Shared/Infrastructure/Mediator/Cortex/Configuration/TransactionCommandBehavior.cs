using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;
using Cortex.Mediator.Commands;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
///     Pipeline behavior that commits the unit of work after a command is handled.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public class TransactionCommandBehavior<TCommand>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionCommandBehavior<TCommand>> logger)
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    /// <inheritdoc />
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken ct)
    {
        await next();
        logger.LogDebug("Committing unit of work for {CommandType}", typeof(TCommand).Name);
        await unitOfWork.CompleteAsync(ct);
    }
}
