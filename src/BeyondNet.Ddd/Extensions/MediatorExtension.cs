namespace BeyondNet.Ddd.Extensions
{
    /// <summary>
    /// Extension methods for the Mediator class.
    /// </summary>
    public static class MediatorExtension
    {
        private const string KeyNameDomainEvents = "DomainEvents";
        private const string KeyNameClearDomainEvents = "ClearDomainEvents";

        /// <summary>
        /// Dispatches the domain events of the specified entity using the mediator.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        /// <param name="entity">The entity object.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, object entity)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var type = entity.GetType();

            var domainEvents = type.GetProperty(KeyNameDomainEvents)?.GetValue(entity) as IEnumerable;

            if (domainEvents != null)
            {
                foreach (var domainEvent in domainEvents)
                {
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
                }

                var clearDomainEvents = type.GetMethod(KeyNameClearDomainEvents);

                if (clearDomainEvents != null)
                {
                    clearDomainEvents.Invoke(entity, null);
                }
            }
        }
    }
}
