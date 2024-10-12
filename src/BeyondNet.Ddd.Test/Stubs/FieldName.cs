using BeyondNet.Ddd.ValueObjects;

namespace  BeyondNet.Ddd.Test.Stubs
{ 
    public class FieldName : StringValueObject
    {
        private FieldName(string value) : base(value)
        {

        }

        public static FieldName Create(string value)
        {
            return new FieldName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }
    }
}
