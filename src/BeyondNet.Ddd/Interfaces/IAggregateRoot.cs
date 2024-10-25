﻿using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an aggregate root in the domain.
    /// </summary>
    public interface IAggregateRoot
    {
        public int Version { get; }
        public IdValueObject Id { get; }
        public DomainEventsManager DomainEvents { get; }
    }
}


