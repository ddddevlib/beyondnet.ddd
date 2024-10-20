using BeyondNet.Ddd.ValueObjects.Common;

namespace BeyondNet.Ddd.Test.Entities
{ 
    public class SampleName : StringValueObject
    {
        private SampleName(string value) : base(value)
        {

        }

        public static SampleName Create(string value)
        {
            return new SampleName(value);
        }
        public static SampleName Default()
        {
            return new SampleName(string.Empty);
        }

        public override void AddValidators()
        {
            base.AddValidators();

            var validator = new SampleNameValidator(this);

            AddValidator(validator);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }
    }
}
