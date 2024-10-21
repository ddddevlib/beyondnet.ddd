using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Services.Interfaces;

namespace BeyondNet.Ddd.Services.Impl
{
    public class DomainEventsManager : IDomainEventsManager
    {
        private List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList().AsReadOnly();
        }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            if (!_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any())
            {
                _domainEvents.Add(eventItem);
            }
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            if (_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any())
            {
                _domainEvents?.Remove(eventItem);
            }
        }

        public void LoadDomainEvents(IReadOnlyCollection<IDomainEvent> history)
        {
            if (history == null) return;
            
            foreach (var domainEvent in history) {
                AddDomainEvent(domainEvent);
            }

            ClearDomainEvents();
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
