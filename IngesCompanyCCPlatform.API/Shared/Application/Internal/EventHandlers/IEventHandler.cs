using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

/// <summary>
///     Handles a domain event published through the mediator.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
