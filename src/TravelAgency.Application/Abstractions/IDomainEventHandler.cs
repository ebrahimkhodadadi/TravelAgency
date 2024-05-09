using MediatR;
using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Application.Abstractions;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}

