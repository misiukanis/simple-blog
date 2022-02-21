namespace Blog.Domain.Core
{
    public abstract class Entity
    {
        private readonly IList<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.AsEnumerable();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
