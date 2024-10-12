namespace BeyondNet.Ddd.ValueObjects
{
    public class PriorityOrder : ValueObject<int>
    {
        public static PriorityOrder DefaultValue => new PriorityOrder(0);

        private PriorityOrder(int value) : base(value)
        {
        }

        public static implicit operator PriorityOrder(int value) => new PriorityOrder(value);

        public static implicit operator int(PriorityOrder priority) => priority.GetValue();

        public static PriorityOrder operator +(PriorityOrder priority, int value) => new PriorityOrder(priority.GetValue() + value);

        public static PriorityOrder operator -(PriorityOrder priority, int value) => new PriorityOrder(priority.GetValue() - value);

        public static PriorityOrder operator ++(PriorityOrder priority) => new PriorityOrder(priority.GetValue() + 1);

        public static PriorityOrder operator --(PriorityOrder priority) => new PriorityOrder(priority.GetValue() - 1);

        public static PriorityOrder FromValue(int value) => new PriorityOrder(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }
    }
}
