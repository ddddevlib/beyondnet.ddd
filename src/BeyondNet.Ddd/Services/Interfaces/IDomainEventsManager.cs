﻿using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Services.Impl
{
    public interface IDomainEventsManager
    {
        int Version { get; set; }
        public IAggregateRoot AggregateRoot { get; }
        void ApplyChange(IDomainEvent domainEvent, bool isNew);
        IReadOnlyCollection<IDomainEvent> GetUncommittedChanges();
        void LoadFromHistory(IReadOnlyCollection<IDomainEvent> history);
        void MarkChangesAsCommitted();
        void ReplayEvents(IEnumerable<IDomainEvent> domainEvents);
    }
}