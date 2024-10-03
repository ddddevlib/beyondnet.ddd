namespace BeyondNet.Ddd.Rules.Interfaces
{
    /// <summary>
    /// Represents a rule validator for a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the subject to be validated.</typeparam>
    public interface IRuleValidator<T>
    {
        /// <summary>
        /// Gets or sets the subject to be validated.
        /// </summary>
        T? Subject { get; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// Validates the rule against the specified context.
        /// </summary>
        /// <param name="context">The rule context.</param>
        /// <returns>A collection of broken rules.</returns>
        ReadOnlyCollection<BrokenRule> Validate(RuleContext context);
    }
}
