using BeyondNet.Ddd.Helpers;
using BeyondNet.Ddd.Rules;

namespace BeyondNet.Ddd.Extensions
{
    public static class BrokenRulesExtension
    {
        private const string BrokenRulesPropertyName = "BrokenRules";

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
            if( instance is null )
                throw new ArgumentNullException(nameof(instance));

            if (properties is null)
                throw new ArgumentNullException(nameof(properties));


            var result = new List<BrokenRule>();

            foreach (var property in properties)
            {
                var isValueObject = ReflectionHelper.IsSubclassOfRawGeneric(typeof(ValueObject<>), property.PropertyType);

                if (isValueObject)
                {
                    var brokenRulesProperty = property.GetValue(instance)?.GetType().GetProperty(BrokenRulesPropertyName);    

                    if (brokenRulesProperty is null)
                    {
                        continue;
                    }

                    var valueObject = property.GetValue(instance);

                    if (valueObject is null)
                    {
                        continue;
                    }

                    var brokenRuleProperty = (BrokenRulesManager)brokenRulesProperty.GetValue(valueObject)!;

                    if (brokenRuleProperty.GetBrokenRules().Any())
                    {
                        result.AddRange(brokenRuleProperty.GetBrokenRules());
                    }
                }
            }

            return result.AsReadOnly();
        }
    }
}
