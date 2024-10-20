namespace BeyondNet.Ddd.ValueObjects
{
    public abstract class DecimalValueObject: ValueObject<decimal>
    {
        protected DecimalValueObject(decimal value) : base(value)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }
    }
}
