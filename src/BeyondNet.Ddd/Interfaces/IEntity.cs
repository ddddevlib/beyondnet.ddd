using BeyondNet.Ddd.Services.Interfaces;

namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an entity in the domain.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProps">The type of the entity properties.</typeparam>
    public interface IEntity<TEntity, TProps> : IDomainEventsManager
            where TEntity : class
            where TProps : class, IProps
    {
    }
}
