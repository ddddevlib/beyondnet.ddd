namespace BeyondNet.Ddd.Interfaces
{
    public interface IDomainEvent : INotification
    {
        public int Version { get; set; }
        DateTime CreatedAt { get; }
        string EventId { get; }
        string EventName { get; }

        string ToString();
    }
}