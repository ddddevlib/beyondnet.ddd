using BeyondNet.Ddd.Rules.Interfaces;

namespace BeyondNet.Ddd.Rules.Impl
{
    /// <summary>
    /// Represents an abstract base class for rule validators.
    /// </summary>
    /// <typeparam name="T">The type of the subject to be validated.</typeparam>
    public abstract class AbstractRuleValidator<T> : IRuleValidator<T> where T : class
    {
        private List<BrokenRule> brokenRules = new List<BrokenRule>();

        /// <summary>
        /// Gets the name of the validator.
        /// </summary>
        public string ValidatorName { get; }

        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        public string RuleName => this.GetType().Name;

        /// <summary>
        /// Gets the subject to be validated.
        /// </summary>
        public T? Subject { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRuleValidator{T}"/> class.
        /// </summary>
        /// <param name="subject">The subject to be validated.</param>
        protected AbstractRuleValidator(T subject, string validatorName)
        {
            Subject = subject;
            ValidatorName = validatorName;
        }

        /// <summary>
        /// Adds the rules to be validated.
        /// </summary>
        /// <param name="context">The rule context.</param>
        public abstract void AddRules(RuleContext? context);

        /// <summary>
        /// Validates the subject against the rules.
        /// </summary>
        /// <param name="context">The rule context.</param>
        /// <returns>A read-only collection of broken rules.</returns>
        public ReadOnlyCollection<BrokenRule> Validate(RuleContext? context)
        {
            if (Subject != null)
            {
                AddRules(context);
            }

            return brokenRules.AsReadOnly();
        }

        /// <summary>
        /// Adds a broken rule to the collection.
        /// </summary>
        /// <param name="propertyName">The name of the property associated with the broken rule.</param>
        /// <param name="message">The error message of the broken rule.</param>
        public void AddBrokenRule(string propertyName, string message)
        {
            brokenRules.Add(new BrokenRule(propertyName, message));
        }
    }
}
