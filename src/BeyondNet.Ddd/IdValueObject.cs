using BeyondNet.Ddd.Rules;

namespace BeyondNet.Ddd
{
    /// <summary>
    /// Represents an identifier value object.
    /// </summary>
    public class IdValueObject : ValueObject<Guid>
    {
        protected IdValueObject(Guid value) : base(value)
        {

        }

        /// <summary>
        /// Creates a new instance of the IdValueObject with a random identifier value.
        /// </summary>
        /// <returns>The newly created IdValueObject.</returns>
        public static IdValueObject Create()
        {
            return new IdValueObject(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a new instance of the IdValueObject with the specified identifier value.
        /// </summary>
        /// <param name="value">The identifier value.</param>
        /// <returns>The newly created IdValueObject.</returns>
        public IdValueObject Load(string value)
        {
            Guid guid = Guid.Empty;

            var isGuidValid = Guid.TryParse(value, out guid);

            if (!isGuidValid)
            {
                BrokenRules.Add(new BrokenRule("IdValueObject", $"Value: {value} has invalid format."));
                return new IdValueObject(guid);
            }

            return new IdValueObject(Guid.Parse(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue();
        }

        /// <summary>
        /// Gets the default value of the IdValueObject, which is an empty identifier value.
        /// </summary>
        public static IdValueObject DefaultValue => new IdValueObject(Guid.Empty);
    }
}