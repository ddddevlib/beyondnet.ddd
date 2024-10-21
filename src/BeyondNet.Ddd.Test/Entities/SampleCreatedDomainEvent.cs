namespace BeyondNet.Ddd.Test.Entities
{
    public record SampleCreatedDomainEvent : DomainEvent
    {
        public SampleCreatedDomainEvent(string aggregateRootId, string name, DateTime Started) 
        {
            AggregateRootId = aggregateRootId;
            Name = name;
            this.Started = Started;
        }

        public string AggregateRootId { get; }
        public string Name { get; }
        public DateTime Started { get; }
    }
}
