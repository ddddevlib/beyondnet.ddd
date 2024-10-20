using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Test.Entities
{
    public class SampleNameValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public SampleNameValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            var value = Subject.GetValue();

            if (string.IsNullOrWhiteSpace(value))
            {
                AddBrokenRule("Value", "The value cannot be null or empty.");
            }

            if (value.Length > 100)
            {
                AddBrokenRule("Value", "The value cannot be longer than 100 characters.");
            }

            if (value.Length < 5)
            {
                AddBrokenRule("Value", "The value cannot be shorter than 5 characters.");
            }
        }
    }
}