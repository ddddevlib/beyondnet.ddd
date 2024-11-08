using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an aggregate root in the domain.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// Gets or sets the version of the aggregate root.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets the unique identifier of the aggregate root.
        /// </summary>
        public IdValueObject Id { get; }

        /// <summary>
        /// Gets the domain events manager associated with the aggregate root.
        /// </summary>
        public DomainEventsManager DomainEvents { get; }
    }
}


