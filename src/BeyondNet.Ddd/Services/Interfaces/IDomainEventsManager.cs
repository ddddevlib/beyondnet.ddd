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
        IReadOnlyCollection<DomainEvent> GetDomainEvents();

        /// <summary>
        /// Adds a domain event to the manager.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        void AddDomainEvent(DomainEvent eventItem);

        /// <summary>
        /// Removes a domain event from the manager.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        void RemoveDomainEvent(DomainEvent eventItem);

        /// <summary>
        /// Clears all domain events from the manager.
        /// </summary>
        void ClearDomainEvents();
    }
}
