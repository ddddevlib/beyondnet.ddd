namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an entity in the domain.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProps">The type of the entity properties.</typeparam>
    public interface IEntity<TEntity, TProps> : IDomainEvents
            where TEntity : class
            where TProps : class, IProps
    {
    }
}
