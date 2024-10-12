namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents the interface for managing domain events.
    /// </summary>
    public interface IDomainEvents
    {
        /// <summary>
        /// Gets the collection of domain events.
        /// </summary>
        /// <returns>The collection of domain events.</returns>
        IReadOnlyCollection<INotification> GetDomainEvents();

        /// <summary>
        /// Clears all domain events.
        /// </summary>
        void ClearDomainEvents();

        /// <summary>
        /// Adds a domain event.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        void AddDomainEvent(INotification eventItem);

        /// <summary>
        /// Removes a domain event.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        void RemoveDomainEvent(INotification eventItem);
    }
}
