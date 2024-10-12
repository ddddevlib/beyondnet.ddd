using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Test.ValueObjects.Validators
{
    public class StringRequiredValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public StringRequiredValidator(ValueObject<string> subject, string validatorName) : base(subject, validatorName)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            if (String.IsNullOrWhiteSpace(Subject!.GetValue()))
            {
                AddBrokenRule($"Property: {nameof(Subject)} Validator: {nameof(StringRequiredValidator)}", "Message: Value is required. It cannot be null, empty or white space");
            }
        }
    }
}
