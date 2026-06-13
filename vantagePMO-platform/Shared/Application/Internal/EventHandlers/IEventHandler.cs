using VantagePMO_platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace VantagePMO_platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}