using System.Text;

namespace BeyondNet.Ddd.Rules
{
    /// <summary>
    /// Represents a collection of broken rules.
    /// </summary>
    public class BrokenRulesManager
    {
        private List<BrokenRule> _brokenRules = new();

        /// <summary>
        /// Adds a broken rule to the collection.
        /// </summary>
        /// <param name="brokenRule">The broken rule to add.</param>
        public  void Add(BrokenRule brokenRule)
        {
            ArgumentNullException.ThrowIfNull(brokenRule, nameof(brokenRule));

            if (!_brokenRules.Any(x => x.Property.ToUpperInvariant() == brokenRule.Property.ToUpperInvariant()
                                       && x.Message.ToUpperInvariant() == brokenRule.Message.ToUpperInvariant()))
            {
                _brokenRules.Add(brokenRule);
            }
        }

        /// <summary>
        /// Adds a collection of broken rules to the collection.
        /// </summary>
        /// <param name="brokenRules">The collection of broken rules to add.</param>
        public void Add(IReadOnlyCollection<BrokenRule> brokenRules)
        {
            ArgumentNullException.ThrowIfNull(brokenRules, nameof(brokenRules));

            if (brokenRules.Any())
            {
                foreach (var brokenRule in brokenRules)
                {
                    Add(brokenRule);
                }
            }
        }

        /// <summary>
        /// Removes a broken rule from the collection.
        /// </summary>
        /// <param name="brokenRule">The broken rule to remove.</param>
        public void Remove(BrokenRule brokenRule)
        {
            ArgumentNullException.ThrowIfNull(brokenRule, nameof(brokenRule));

            if (_brokenRules.Contains(brokenRule))
            {
                _brokenRules.Remove(brokenRule);
            }
        }

        /// <summary>
        /// Clears all the broken rules from the collection.
        /// </summary>
        public void Clear()
        {
            if (_brokenRules.Any())
            {
                _brokenRules.Clear();
            }
        }

        /// <summary>
        /// Gets the read-only collection of broken rules.
        /// </summary>
        /// <returns>The read-only collection of broken rules.</returns>
        public ReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            return _brokenRules.AsReadOnly();
        }

        /// <summary>
        /// Gets the broken rules of the entity as a string.
        /// </summary>
        /// <returns>The broken rules of the entity as a string.</returns>
        public string GetBrokenRulesAsString()
        {
            if (!_brokenRules.Any()) return string.Empty;

            var sb = new StringBuilder();

            foreach (var rule in _brokenRules)
            {
                var line = $"Property: {rule.Property}, Message: {rule.Message}";

                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
