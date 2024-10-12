using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.ValueObjects.Validators
{
    /// <summary>
    /// Validator for ensuring that a DateTime value is in UTC format and in the future.
    /// </summary>
    public class DateTimeUtcValidator : AbstractRuleValidator<ValueObject<DateTime>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeUtcValidator"/> class.
        /// </summary>
        /// <param name="subject">The value object to validate.</param>
        public DateTimeUtcValidator(ValueObject<DateTime> subject, string validatorName) : base(subject, validatorName)
        {
        }

        /// <summary>
        /// Adds the validation rules for the DateTime value.
        /// </summary>
        /// <param name="context">The rule context.</param>
        public override void AddRules(RuleContext? context)
        {
            if (Subject!.GetValue().Kind != DateTimeKind.Utc)
            {
                AddBrokenRule("Value", "Value must be in UTC format");
            }

            if (Subject.GetValue() < DateTime.Now.AddDays(1))
            {
                AddBrokenRule("Value", "Value must be in the future");
            }
        }
    }
}
