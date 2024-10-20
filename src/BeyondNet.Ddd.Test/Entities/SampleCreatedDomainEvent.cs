namespace BeyondNet.Ddd.Test.Entities
{
    public record SampleCreatedDomainEvent : DomainEvent
    {
        public SampleCreatedDomainEvent(string eventName) : base(eventName)
        {
        }
    }
}
