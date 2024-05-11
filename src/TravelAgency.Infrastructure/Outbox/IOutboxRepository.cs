using TravelAgency.Domain.Common.BaseTypes.Abstractions;

namespace TravelAgency.Infrastructure.Outbox;

public interface IOutboxRepository : IRepository
{
    Task<OutboxMessage[]> GetOutboxMessagesAsync(CancellationToken cancellationToken);

    Task<bool> IsConsumerAlreadyProcessed(IDomainEvent domainEvent, string consumer, CancellationToken cancellationToken);

    Task AddOutboxMessageConsumer(IDomainEvent domainEvent, string consumer);

    void PersistOutboxMessagesFromDomainEvents();
}