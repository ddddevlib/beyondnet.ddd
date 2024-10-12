using BeyondNet.Ddd.ValueObjects.Validators;

namespace BeyondNet.Ddd.valueObjects
{
    public class Email : ValueObject<string>
    {
        public Email(string value) : base(value)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new EmailValidator(this));

        }

    }
}
