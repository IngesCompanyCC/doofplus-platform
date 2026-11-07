using Cortex.Mediator.Commands;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
///     Pipeline behavior that logs when a command starts and when it finishes.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public class LoggingCommandBehavior<TCommand>(
    ILogger<LoggingCommandBehavior<TCommand>> logger)
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    /// <inheritdoc />
    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken ct)
    {
        var commandName = typeof(TCommand).Name;
        logger.LogInformation("Executing command {CommandName}", commandName);
        await next();
        logger.LogInformation("Executed command {CommandName}", commandName);
    }
}
