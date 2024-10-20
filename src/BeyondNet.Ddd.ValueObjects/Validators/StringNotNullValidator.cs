using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.ValueObjects.Validators
{
    public class StringNotNullValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public StringNotNullValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            var value = Subject.GetValue();

            if (value == null)
            {
                AddBrokenRule("CommonValidator", "Value for property cannot be null");
                return;
            }

        }
    }
}
