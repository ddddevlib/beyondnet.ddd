using BeyondNet.Ddd.ValueObjects.Validators;

namespace BeyondNet.Ddd.ValueObjects
{
    /// <summary>
    /// Represents a value object that stores a DateTime value in UTC format.
    /// </summary>
    /// <remarks>
    /// This value object ensures that the stored DateTime value is always in UTC format.
    /// </remarks>
    public class DateTimeUtcValueObject : ValueObject<DateTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeUtcValueObject"/> class.
        /// </summary>
        /// <param name="value">The DateTime value to be stored.</param>
        protected DateTimeUtcValueObject(DateTime value) : base(value)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="DateTimeUtcValueObject"/> class.
        /// </summary>
        /// <param name="value">The DateTime value to be stored.</param>
        /// <returns>A new instance of the <see cref="DateTimeUtcValueObject"/> class.</returns>
        public static DateTimeUtcValueObject Create(DateTime value)
        {
            return new DateTimeUtcValueObject(value);
        }

        /// <summary>
        /// Adds validators to the value object.
        /// </summary>
        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new DateTimeUtcValidator(this));
        }

        /// <summary>
        /// Gets the equality components of the value object.
        /// </summary>
        /// <returns>An enumerable collection of the equality components.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }

        /// <summary>
        /// Gets the default value of the <see cref="DateTimeUtcValueObject"/> class.
        /// </summary>
        public static DateTimeUtcValueObject DefaultValue => new DateTimeUtcValueObject(DateTime.UtcNow);
    }
}
