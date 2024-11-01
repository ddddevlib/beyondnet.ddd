namespace BeyondNet.Ddd.Interfaces
{
    public interface IMetadata
    {
        Guid EventId { get; }
        string EventName { get; }        
        Guid AggregateId { get; }
        string AggregateName { get; }
        
        void SetMetadata(Guid eventId, string eventName, Guid aggregateId, string aggregateName);
    }
}
