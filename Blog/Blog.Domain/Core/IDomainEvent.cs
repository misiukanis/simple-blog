using MediatR;

namespace Blog.Domain.Core
{
    public interface IDomainEvent : INotification
    {
        DateTime DateOccurred { get; }
    }
}
