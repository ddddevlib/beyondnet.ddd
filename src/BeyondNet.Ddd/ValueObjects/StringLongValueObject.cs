using BeyondNet.Ddd.ValueObjects.Validators;

namespace BeyondNet.Ddd.ValueObjects
{
    public class StringLongValueObject : ValueObject<string>
    {
        private readonly int minLenght;
        private readonly int maxLenght;

        public StringLongValueObject(string value, int minLenght, int maxLenght) : base(value)
        {
            this.minLenght = minLenght;
            this.maxLenght = maxLenght;
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new StringRequiredValidator(this));

            AddValidator(new StringMaxLenghtValidator(this, maxLenght));

            AddValidator(new StringMinLenghtValidator(this, minLenght));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
