namespace BeyondNet.Ddd.Interfaces
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
        string EventId { get; }
        string EventName { get; }

        string ToString();
    }
}