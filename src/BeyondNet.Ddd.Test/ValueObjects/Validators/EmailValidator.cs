﻿
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;


namespace BeyondNet.Ddd.ValueObjects.Validators
{
    public class EmailValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public EmailValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            var value = Subject!.GetValue();

            if (string.IsNullOrWhiteSpace(value))
            {
                AddBrokenRule("Email", "Email is required");
            }
        }
    }
}
