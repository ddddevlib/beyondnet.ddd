using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.ValueObjects.Validators
{
    public class StringMaxLenghtValidator : AbstractRuleValidator<ValueObject<string>>
    {
        private readonly int maxLenght;

        public StringMaxLenghtValidator(ValueObject<string> subject, int maxLenght) : base(subject)
        {
            this.maxLenght = maxLenght;
        }

        public override void AddRules(RuleContext context)
        {
            var currentLenght = Subject!.GetValue().Length;

            if (currentLenght > this.maxLenght)
            {
                AddBrokenRule("Value", $"Current Lenght {currentLenght} cannot greather than {maxLenght}");
            }
        }
    }
}
