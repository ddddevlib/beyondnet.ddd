using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Test.ValueObjects.Validators
{
    public class StringRequiredValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public StringRequiredValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            if (String.IsNullOrWhiteSpace(Subject!.GetValue()))
            {
                AddBrokenRule("Value", "Value is required");
            }
        }
    }
}
