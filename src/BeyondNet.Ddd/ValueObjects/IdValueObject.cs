namespace BeyondNet.Ddd.ValueObjects
{
    /// <summary>
    /// Represents an identifier value object.
    /// </summary>
    public class IdValueObject : ValueObject<string>
    {
        protected IdValueObject(string value) : base(value)
        {

        }

        /// <summary>
        /// Creates a new instance of the IdValueObject with a random identifier value.
        /// </summary>
        /// <returns>The newly created IdValueObject.</returns>
        public static IdValueObject Create()
        {
            return new IdValueObject(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Creates a new instance of the IdValueObject with the specified identifier value.
        /// </summary>
        /// <param name="value">The identifier value.</param>
        /// <returns>The newly created IdValueObject.</returns>
        public static IdValueObject Create(string value)
        {
            return new IdValueObject(Guid.Parse(value).ToString());
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }

        /// <summary>
        /// Gets the default value of the IdValueObject, which is an empty identifier value.
        /// </summary>
        public static IdValueObject DefaultValue => new IdValueObject(Guid.Empty.ToString());
    }
}