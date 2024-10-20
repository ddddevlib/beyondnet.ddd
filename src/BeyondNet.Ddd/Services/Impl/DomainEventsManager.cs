using BeyondNet.Ddd.Services.Interfaces;

namespace BeyondNet.Ddd.Services.Impl
{
    public class DomainEventsManager : IDomainEventsManager
    {
        private List<DomainEvent> _domainEvents = [];

        public IReadOnlyCollection<DomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList().AsReadOnly();
        }

        public void AddDomainEvent(DomainEvent eventItem)
        {
            if (!_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any())
            {
                _domainEvents.Add(eventItem);
            }
        }

        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            if (_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any())
            {
                _domainEvents?.Remove(eventItem);
            }
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
