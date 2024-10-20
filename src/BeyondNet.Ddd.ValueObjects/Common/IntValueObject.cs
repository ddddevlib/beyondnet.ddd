namespace BeyondNet.Ddd.ValueObjects.Common
{
    public abstract class IntValueObject : ValueObject<int>
    {
        protected IntValueObject(int value) : base(value)
        {
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }
    }
}
