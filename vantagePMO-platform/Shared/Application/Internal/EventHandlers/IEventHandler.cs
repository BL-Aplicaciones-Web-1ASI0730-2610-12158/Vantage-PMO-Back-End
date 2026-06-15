using vantagePMO_platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace vantagePMO_platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}