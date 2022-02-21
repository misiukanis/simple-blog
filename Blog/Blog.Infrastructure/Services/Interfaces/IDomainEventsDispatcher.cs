using Blog.Domain.Core;

namespace Blog.Infrastructure.Services.Interfaces
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents);
    }
}
