namespace BeyondNet.Ddd.ValueObjects
{
    /// <summary>
    /// Represents a string value object.
    /// </summary>
    public class StringValueObject : ValueObject<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueObject"/> class.
        /// </summary>
        /// <param name="value">The string value.</param>
        protected StringValueObject(string value) : base(value)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StringValueObject"/> class.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The created <see cref="StringValueObject"/> instance.</returns>
        public static StringValueObject Create(string value)
        {
            return new StringValueObject(value);
        }

        /// <summary>
        /// Gets the equality components of the string value object.
        /// </summary>
        /// <returns>An enumerable of the equality components.</returns>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }

        /// <summary>
        /// Gets the default value of the string value object.
        /// </summary>
        public static StringValueObject DefaultValue => new StringValueObject(string.Empty);
    }
}
