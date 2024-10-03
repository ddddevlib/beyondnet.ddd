using BeyondNet.Ddd.Helpers;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Rules;

namespace BeyondNet.Ddd.Extensions
{
    public static class EntityExtension
    {
        private const string BrokenRulesKeyFunctionName = "GetBrokenRules";

        /// <summary>
        /// Gets the broken rules for the properties of the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProps">The type of the properties.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="properties">The properties to check for broken rules.</param>
        /// <returns>A read-only collection of broken rules.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="entity"/> or <paramref name="properties"/> is null.</exception>
        public static ReadOnlyCollection<BrokenRule> GetPropertiesBrokenRules<TEntity, TProps>(this Entity<TEntity, TProps> entity,
                                                                                               PropertyInfo[] properties)
            where TEntity : Entity<TEntity, TProps>
            where TProps : class, IProps
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var result = new List<BrokenRule>();

            foreach (var property in properties)
            {
                var isValueObject = ReflectionHelper.IsSubclassOfRawGeneric(typeof(ValueObject<>), property.PropertyType);

                if (isValueObject)
                {
                    var method = property.PropertyType.GetMethod(BrokenRulesKeyFunctionName);

                    if (method != null)
                    {
                        var valueObject = property.GetValue(entity);

                        if (valueObject == null)
                        {
                            continue;
                        }

                        var brokenRules = (ReadOnlyCollection<BrokenRule>)method.Invoke(valueObject, null)!;

                        if (brokenRules.Any())
                        {
                            foreach (var brokenRule in brokenRules)
                            {
                                var isDuplicated = result.Any(x => x.Property.ToUpperInvariant() == brokenRule.Property.ToUpperInvariant()
                                                                && x.Message.ToUpperInvariant() == brokenRule.Message.ToUpperInvariant());
                                if (!isDuplicated)
                                {
                                    result.Add(brokenRule);
                                }
                            }
                        }
                    }
                }
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Gets the broken rules for the properties of the specified entity instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="properties">The properties to check for broken rules.</param>
        /// <param name="instance">The instance of the entity.</param>
        /// <returns>A read-only collection of broken rules.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="properties"/> is null.</exception>
        public static ReadOnlyCollection<BrokenRule> GetPropertiesBrokenRules<TEntity>(this PropertyInfo[] properties,
                                                                                       TEntity instance)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            var result = new List<BrokenRule>();

            foreach (var property in properties)
            {
                var isValueObject = ReflectionHelper.IsSubclassOfRawGeneric(typeof(ValueObject<>), property.PropertyType);
                if (isValueObject)
                {
                    var method = property.PropertyType.GetMethod(BrokenRulesKeyFunctionName);
                    if (method != null)
                    {
                        var valueObject = property.GetValue(instance);

                        if (valueObject == null)
                        {
                            continue;
                        }

                        var brokenRules = (ReadOnlyCollection<BrokenRule>)method.Invoke(valueObject, null)!;

                        if (brokenRules.Any())
                        {
                            result.AddRange(brokenRules);
                        }
                    }
                }
            }

            return result.AsReadOnly();
        }
    }
}
