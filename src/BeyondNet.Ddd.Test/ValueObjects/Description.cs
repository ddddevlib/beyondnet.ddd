
using BeyondNet.Ddd.Test.ValueObjects.Validators;

namespace BeyondNet.Ddd.ValueObjects
{
    public class Description : StringValueObject
    {
        protected Description(string value) : base(value)
        {
        }

        public static new Description Create(string value)
        {
            return new Description(value);
        }

        public override void AddValidators()
        {
            base.AddValidators();
            
            AddValidator(new StringRequiredValidator(this, nameof(NameValidator)));
        }

        public static Description DefaultValue => new Description(string.Empty);
    }
}
