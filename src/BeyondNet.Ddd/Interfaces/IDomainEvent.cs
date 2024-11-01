namespace BeyondNet.Ddd.Interfaces
{
    public interface IDomainEvent : INotification
    {
        IMetadata Metadata { get; }               
        void SetMetadata(IMetadata metadata);
    }
}