﻿using BeyondNet.Ddd;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Test.ValueObjects.Validators;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.ValueObjects
{
    public class Name : StringValueObject
    {
        protected Name(string value) : base(value)
        {

        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new NameValidator(this));
            AddValidator(new StringRequiredValidator(this));
        }

        public static Name Create(string value)
        {
            return new Name(value);
        }

    }

    public class NameValidator : AbstractRuleValidator<ValueObject<string>>
    {
        public NameValidator(ValueObject<string> subject) : base(subject)
        {
        }

        public override void AddRules(RuleContext context)
        {
            if (Subject!.GetValue().ToString()!.Length < 3 && Subject!.GetValue().ToString()!.Length > 300)
            {
                AddBrokenRule("Value", "Value must be between 3 and 300 characters");
            }
        }
    }
}
