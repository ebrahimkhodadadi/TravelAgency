using MediatR;

namespace TravelAgency.Domain.Common.BaseTypes.Abstractions;

public interface IDomainEvent : INotification
{
    Ulid Id { get; init; }
}