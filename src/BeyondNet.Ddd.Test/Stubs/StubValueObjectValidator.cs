using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Test.Stubs
{
    public class StubValueObjectValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public StubValueObjectValidator(ValueObject<string> subject, string validatorName) : base(subject, validatorName)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            AddBrokenRule("Value", "Value is invalid");
        }
    }
}
