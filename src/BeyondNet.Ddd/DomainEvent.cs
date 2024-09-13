using MediatR;

namespace BeyondNet.Ddd
{
    public abstract record DomainEvent : INotification
    {
        public string EventId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public DomainEvent()
        {
            EventId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
