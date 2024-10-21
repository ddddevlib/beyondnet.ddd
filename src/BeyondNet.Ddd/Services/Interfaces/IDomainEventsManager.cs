using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Services.Interfaces
{
    /// <summary>
    /// Represents the interface for managing domain events.
    /// </summary>
    public interface IDomainEventsManager
    {
        /// <summary>
        /// Gets the collection of domain events.
        /// </summary>
        /// <returns>The collection of domain events.</returns>
        IReadOnlyCollection<IDomainEvent> GetDomainEvents();

        /// <summary>
        /// Adds a domain event to the manager.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        void AddDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Removes a domain event from the manager.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        void RemoveDomainEvent(IDomainEvent eventItem);

        /// <summary>
        /// Load domain events into the manager.
        /// </summary>
        /// <param name="history"></param>
        void LoadDomainEvents(IReadOnlyCollection<IDomainEvent> history);

        /// <summary>
        /// Clears all domain events from the manager.
        /// </summary>
        void ClearDomainEvents();
    }
}
