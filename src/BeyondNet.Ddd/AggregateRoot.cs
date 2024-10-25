using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd
{

    /// <summary>
    /// Represents the base class for aggregate roots in the domain-driven design.
    /// </summary>
    /// <typeparam name="TAggegateRoot">The type of the aggregate root.</typeparam>
    /// <typeparam name="TProps">The type of the properties of the aggregate root.</typeparam>
    public abstract class AggregateRoot<TAggegateRoot, TProps> : Entity<TAggegateRoot, TProps>, IAggregateRoot 
                       where TAggegateRoot : class
                       where TProps : class, IProps
    {
        public int Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TAggegateRoot, TProps}"/> class.
        /// </summary>
        /// <param name="props">The properties of the aggregate root.</param>
        protected AggregateRoot(TProps props) : base(props)
        {
            DomainEvents = new DomainEventsManager();
        }

        #region DomainEvents    

        /// <summary>
        /// The domain events associated with the entity.
        /// </summary>
        public DomainEventsManager DomainEvents { get; set; }

        #endregion
    }
}