using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Services.Impl
{
    /// <summary>
    /// Manages domain events, including tracking uncommitted changes and applying events.
    /// </summary>
    public class DomainEventsManager : IDomainEventsManager
    {
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Gets or sets the version of the domain events manager.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventsManager"/> class.
        /// </summary>
        public DomainEventsManager()
        {
            _domainEvents = new List<IDomainEvent>();
            Version = -1;
        }

        /// <summary>
        /// Gets the uncommitted changes.
        /// </summary>
        /// <returns>A read-only collection of uncommitted domain events.</returns>
        public IReadOnlyCollection<IDomainEvent> GetUncommittedChanges()
        {
            return _domainEvents.AsReadOnly();
        }

        /// <summary>
        /// Marks all changes as committed.
        /// </summary>
        public void MarkChangesAsCommitted()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Loads the domain events from history.
        /// </summary>
        /// <param name="history">The history of domain events.</param>
        /// <exception cref="ArgumentNullException">Thrown when history is null.</exception>
        public void LoadFromHistory(IReadOnlyCollection<IDomainEvent> history)
        {
            if (history is null)
            {
                throw new ArgumentNullException(nameof(history));
            }

            foreach (var e in history)
            {
                ApplyChange(e, false);
            }
        }

        /// <summary>
        /// Applies a domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to apply.</param>
        /// <param name="isNew">Indicates whether the event is new.</param>
        /// <exception cref="ArgumentNullException">Thrown when domainEvent is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the Apply method is missing for the domain event type.</exception>
        public void ApplyChange(IDomainEvent domainEvent, bool isNew)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            var method = GetType().GetMethod("Apply", new Type[] { domainEvent.GetType() });

            if (method == null)
            {
                throw new InvalidOperationException($"Missing Apply method for {domainEvent.GetType()}");
            }

            method.Invoke(this, new object[] { domainEvent });

            if (isNew)
            {
                _domainEvents.Add(domainEvent);
            }
        }

        /// <summary>
        /// Raises a domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to raise.</param>
        /// <exception cref="ArgumentNullException">Thrown when domainEvent is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "<Pending>")]
        public void RaiseEvent(IDomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            ApplyChange(domainEvent, true);
        }

        /// <summary>
        /// Applies a domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to apply.</param>
        public void ApplyChange(IDomainEvent domainEvent)
        {
            ApplyChange(domainEvent, true);
        }

        /// <summary>
        /// Replays a collection of domain events.
        /// </summary>
        /// <param name="domainEvents">The domain events to replay.</param>
        /// <exception cref="ArgumentNullException">Thrown when domainEvents is null.</exception>
        public void ReplayEvents(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents is null)
            {
                throw new ArgumentNullException(nameof(domainEvents));
            }

            foreach (var @event in domainEvents)
            {
                ApplyChange(@event);
            }
        }
    }
}
