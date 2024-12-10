using Blog.Domain.Core;
using Blog.Infrastructure.Services.Interfaces;
using MediatR;

namespace Blog.Infrastructure.Services
{
    public class DomainEventsDispatcher(IPublisher mediator) : IDomainEventsDispatcher
    {
        private readonly IPublisher _mediator = mediator;

        public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
