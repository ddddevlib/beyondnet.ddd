using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.Test.Entities
{
    public class SampleEntityValidator : AbstractRuleValidator<SampleEntity>
    {
        public SampleEntityValidator(SampleEntity subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext? context)
        {
            throw new NotImplementedException();
        }
    }
}
