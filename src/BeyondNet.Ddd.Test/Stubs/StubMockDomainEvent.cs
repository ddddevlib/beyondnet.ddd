namespace BeyondNet.Ddd.Test.Stubs
{
    public record StubMockDomainEvent : DomainEvent
    {
        public StubMockDomainEvent(string eventName) : base(eventName)
        {
        }
    }
}
