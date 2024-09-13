using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;

namespace BeyondNet.Ddd.ValueObjects.Validators
{
    public class StringMinLenghtValidator : AbstractRuleValidator<ValueObject<string>>
    {
        private readonly int minLenght;

        public StringMinLenghtValidator(ValueObject<string> subject, int maxLenght) : base(subject)
        {
            this.minLenght = maxLenght;
        }

        public override void AddRules(RuleContext context)
        {
            var currentLenght = Subject!.GetValue().Length;

            if (currentLenght < this.minLenght)
            {
                AddBrokenRule("Value", $"Current Lenght {currentLenght} cannot less than {minLenght}");
            }
        }
    }
}
