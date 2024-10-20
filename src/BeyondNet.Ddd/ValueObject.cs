using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Rules.PropertyChange;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd
{
    /// <summary>
    /// Base class for value objects in the domain-driven design.
    /// </summary>
    /// <typeparam name="TValue">The type of the value object.</typeparam>
    public abstract class ValueObject<TValue> : AbstractNotifyPropertyChanged, IProps
    {
        #region Members

        public TrackingManager Tracking { get; private set; }

        private ValidatorRuleManager<AbstractRuleValidator<ValueObject<TValue>>> _validatorRules = new();

        private BrokenRulesManager _brokenRules = new BrokenRulesManager();

        public bool IsValid => !_brokenRules.GetBrokenRules().Any();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value object.
        /// </summary>
        protected TValue Value
        {
            get
            {
                return (TValue)GetValue();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueObject{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value of the value object.</param>
        protected ValueObject(TValue value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            RegisterProperty(nameof(Value), typeof(TValue), value, ValuePropertyChanged);

            Tracking = TrackingManager.MarkNew();

#pragma warning disable CA2214 // Do not call overridable methods in constructors
            AddValidators();
#pragma warning restore CA2214 // Do not call overridable methods in constructors

            Validate();
        }

        #endregion

        #region Methods

        private void ValuePropertyChanged(AbstractNotifyPropertyChanged sender, NotifyPropertyChangedContextArgs e)
        {
            Tracking = TrackingManager.MarkDirty();

            Validate();
        }

        public void SetValue(TValue value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            SetValue(value, nameof(Value));
        }

        public TValue GetValue()
        {
            return (TValue)GetValue(nameof(Value));
        }

        #endregion

        #region Business Rules

        /// <summary>
        /// Adds the validators for the value object.
        /// </summary>
        public virtual void AddValidators()
        {

        }

        private void Validate()
        {
            _brokenRules.Clear();

            _brokenRules.Add(_validatorRules.GetBrokenRules());
        }

        public void AddValidator(AbstractRuleValidator<ValueObject<TValue>> validator)
        {
            _validatorRules.Add(validator);
        }

        public void AddValidators(ICollection<AbstractRuleValidator<ValueObject<TValue>>> validators)
        {
            _validatorRules.Add(validators);
        }

        public void RemoveValidator(AbstractRuleValidator<ValueObject<TValue>> validator)
        {
            _validatorRules.Remove(validator);
        }


        public IReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            return this._brokenRules.GetBrokenRules();
        }

        #endregion

        #region Equality

        protected static bool EqualOperator(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right!);
        }

        protected static bool NotEqualOperator(ValueObject<TValue> left, ValueObject<TValue> right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether the current value object is equal to another value object.
        /// </summary>
        /// <param name="obj">The value object to compare with the current value object.</param>
        /// <returns><c>true</c> if the current value object is equal to the other value object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject<TValue>)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Gets the hash code of the value object.
        /// </summary>
        /// <returns>The hash code of the value object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Creates a copy of the value object.
        /// </summary>
        /// <returns>A copy of the value object.</returns>
        public ValueObject<TValue> GetCopy()
        {
            return (ValueObject<TValue>)MemberwiseClone();
        }

        public object Clone()
        {
            return GetCopy();
        }


        #endregion
    }
}
