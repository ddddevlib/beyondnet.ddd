using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd
{

    public abstract class AggregateRoot<TAggegateRoot, TProps> : Entity<TAggegateRoot, TProps>
                       where TAggegateRoot : class
                       where TProps : class, IProps
    {
        protected AggregateRoot(TProps props) : base(props)
        {

            _domainEvents = new DomainEventsManager();

            _version = 0;
        }

        #region DomainEvents    

        /// <summary>
        /// The version of the entity.
        /// </summary>
        private int _version;

        /// <summary>
        /// The domain events associated with the entity.
        /// </summary>
        private DomainEventsManager _domainEvents = new();

        /// <summary>
        /// Gets or sets the version of the entity.
        /// </summary>
        public int Version
        {
            get { return _version; }
            private set { _version = value; }
        }


        /// <summary>
        /// Sets the version of the entity.
        /// </summary>
        /// <param name="version">The version to set.</param>
        public void SetVersion(int version)
        {
            if (version <= 0)
            {
                return;
            }

            _version = version;
        }

        /// <summary>
        /// Gets the domain events associated with the entity.
        /// </summary>
        /// <returns>The domain events associated with the entity.</returns>
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.GetDomainEvents().ToList().AsReadOnly();
        }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.AddDomainEvent(eventItem);
            Version++;
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.RemoveDomainEvent(eventItem);
            Version--;
        }

        public void LoadDomainEvents(IReadOnlyCollection<IDomainEvent> history)
        {
            _domainEvents.LoadDomainEvents(history);
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.ClearDomainEvents();
        }

        #endregion
    }
}