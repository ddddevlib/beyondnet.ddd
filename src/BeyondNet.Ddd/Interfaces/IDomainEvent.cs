namespace BeyondNet.Ddd.Interfaces
{
    public interface IDomainEvent : INotification
    {
        public int Version { get; }
        DateTime CreatedAt { get; }
        string EventId { get; }
        string EventName { get; }

        string ToString();
    }
}