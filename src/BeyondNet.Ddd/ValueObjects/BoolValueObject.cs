namespace BeyondNet.Ddd.ValueObjects
{
    public abstract class BoolValueObject : ValueObject<bool>
    {
        protected BoolValueObject(bool value) : base(value)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }        
    }
}
