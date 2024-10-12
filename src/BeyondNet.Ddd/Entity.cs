using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Extensions;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Impl;
using System.Text;

namespace BeyondNet.Ddd
{
    /// <summary>
    /// Represents an abstract base class for entities in the domain-driven design.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProps">The type of the entity properties.</typeparam>
    public abstract class Entity<TEntity, TProps>
            where TEntity : Entity<TEntity, TProps>
            where TProps : class, IProps
    {
        #region Members         

        private List<INotification> _domainEvents = new List<INotification>();

        private ValidatorRules<Entity<TEntity, TProps>> _validatorRules = new ValidatorRules<Entity<TEntity, TProps>>();

        private BrokenRules _brokenRules = new BrokenRules();

        private TProps? _props;

        private int _version;

        #endregion

        #region Properties

        public Tracking Tracking { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the entity is valid.
        /// </summary>
        /// <returns><c>true</c> if the entity is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            Validate();

            return !_brokenRules.GetBrokenRules().Any();
        }

        /// <summary>
        /// Gets a value indicating whether the entity is new.
        /// </summary>
        public bool IsNew => Tracking.IsNew;

        /// <summary>
        /// Gets a value indicating whether the entity is dirty.
        /// </summary>
        public bool IsDirty => Tracking.IsDirty;

        /// <summary>
        /// Gets or sets the version of the entity.
        /// </summary>
        public int Version
        {
            get { return _version; }
            private set { _version = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntity, TProps}"/> class.
        /// </summary>
        /// <param name="props">The properties of the entity.</param>
        protected Entity(TProps props)
        {
            _brokenRules = new BrokenRules();

            _version = 0;

            _props = props;

            Validate();

            Tracking = Tracking.MarkNew();
        }

        #endregion

        #region Methods

        public ValidatorRules<Entity<TEntity, TProps>> Validators => _validatorRules;


        /// <summary>
        /// Gets a copy of the entity properties.
        /// </summary>
        /// <returns>A copy of the entity properties.</returns>
        public TProps GetPropsCopy()
        {
            var copyProps = _props!.Clone();

            return (TProps)copyProps;
        }

        /// <summary>
        /// Gets the properties of the entity.
        /// </summary>
        public TProps Props
        {
            get { return _props!; }
        }

        /// <summary>
        /// Sets the properties of the entity.
        /// </summary>
        /// <param name="props">The properties to set.</param>
        public void SetProps(TProps props)
        {
            _props = props;
            
            Tracking = Tracking.MarkDirty(props);
        }

        /// <summary>
        /// Sets the version of the entity.
        /// </summary>
        /// <param name="version">The version to set.</param>
        public void SetVersion(int version)
        {
            if (version <= 0)
            {
                return;
            }

            _version = version;
        }

        #endregion

        #region DomainEvents                        

        /// <summary>
        /// Gets the domain events associated with the entity.
        /// </summary>
        /// <returns>The domain events associated with the entity.</returns>
        public IReadOnlyCollection<INotification> GetDomainEvents()
        {
            return _domainEvents.ToList().AsReadOnly();
        }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion

        #region Business Rules

        /// <summary>
        /// Validates the entity.
        /// </summary>
        public void Validate()
        {
            AddValidators();

            _brokenRules.Add(_validatorRules.GetBrokenRules().ToList());

            var props = GetPropsCopy().GetType().GetProperties();

            var propsBrokenRules = props.GetPropertiesBrokenRules(_props);

            if (propsBrokenRules.Any())
            {
                _brokenRules.Add(propsBrokenRules);
            }
        }

        /// <summary>
        /// Adds custom validators for the entity.
        /// </summary>
        public virtual void AddValidators() { }

        /// <summary>
        /// Adds a validator for the entity.
        /// </summary>
        /// <param name="validator">The validator to add.</param>
        public void AddValidator(AbstractRuleValidator<Entity<TEntity, TProps>> validator)
        {
            _validatorRules.Add(validator);
        }

        /// <summary>
        /// Adds multiple validators for the entity.
        /// </summary>
        /// <param name="validators">The validators to add.</param>
        public void AddValidators(ICollection<AbstractRuleValidator<Entity<TEntity, TProps>>> validators)
        {
            _validatorRules.Add(validators);
        }

        /// <summary>
        /// Removes a validator from the entity.
        /// </summary>
        /// <param name="validator">The validator to remove.</param>
        public void RemoveValidator(AbstractRuleValidator<Entity<TEntity, TProps>> validator)
        {
            _validatorRules.Remove(validator);
        }

        /// <summary>
        /// Gets the broken rules of the entity.
        /// </summary>
        /// <returns>The broken rules of the entity.</returns>
        public ReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            return _brokenRules.GetBrokenRules();
        }

        public string GetBrokenRulesAsString()
        {
            var sb = new StringBuilder();
            
            foreach (var rule in _brokenRules.GetBrokenRules()) {
                var line = $"Property: {rule.Property}, Message: {rule.Message}";

                sb.Append(line);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Adds a broken rule to the entity.
        /// </summary>
        /// <param name="propertyName">The name of the property with the broken rule.</param>
        /// <param name="message">The message of the broken rule.</param>
        public void AddBrokenRule(string propertyName, string message)
        {
            _brokenRules.Add(new BrokenRule(propertyName, message));
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity<TEntity, TProps>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether two entities are equal.
        /// </summary>
        /// <param name="left">The left entity.</param>
        /// <param name="right">The right entity.</param>
        /// <returns><c>true</c> if the entities are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Entity<TEntity, TProps> left, Entity<TEntity, TProps> right)
        {
            if (Equals(left, null))
                return Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two entities are not equal.
        /// </summary>
        /// <param name="left">The left entity.</param>
        /// <param name="right">The right entity.</param>
        /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Entity<TEntity, TProps> left, Entity<TEntity, TProps> right)
        {
            return !(left == right);
        }

        #endregion
    }
}

