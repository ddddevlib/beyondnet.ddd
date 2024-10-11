﻿namespace BeyondNet.Ddd
{
    /// <summary>
    /// Represents a domain event.
    /// </summary>
    public abstract record DomainEvent : INotification
    {
        /// <summary>
        /// Gets the unique identifier of the event.
        /// </summary>
        public string EventId { get; private set; }

        /// <summary>
        /// Gets the timestamp when the event was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        protected DomainEvent()
        {
            EventId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
