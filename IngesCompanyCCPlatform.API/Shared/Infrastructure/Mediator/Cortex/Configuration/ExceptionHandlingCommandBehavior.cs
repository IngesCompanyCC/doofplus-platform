using Cortex.Mediator.Commands;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
///     Pipeline behavior that logs the errors raised while a command is handled and lets them
///     reach the global exception handler.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public class ExceptionHandlingCommandBehavior<TCommand>(
    ILogger<ExceptionHandlingCommandBehavior<TCommand>> logger)
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    /// <inheritdoc />
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken ct)
    {
        try
        {
            await next();
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Command {CommandName} was cancelled.", typeof(TCommand).Name);
            throw;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update error during command {CommandName}.", typeof(TCommand).Name);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled error during command {CommandName}.", typeof(TCommand).Name);
            throw;
        }
    }
}
