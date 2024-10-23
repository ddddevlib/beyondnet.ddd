namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an aggregate root in the domain.
    /// </summary>
    public interface IAggregateRoot
    {
        public IdValueObject Id { get; }
        int Version { get; }

        void AddDomainEvent(IDomainEvent eventItem);
        void ClearDomainEvents();
        IReadOnlyCollection<IDomainEvent> GetDomainEvents();
        void LoadDomainEvents(IReadOnlyCollection<IDomainEvent> history);
        void RemoveDomainEvent(IDomainEvent eventItem);
        void SetVersion(int version);
    }
}
