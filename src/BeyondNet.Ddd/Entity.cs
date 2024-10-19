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

        /// <summary>
        /// The domain events associated with the entity.
        /// </summary>
        private List<DomainEvent> _domainEvents = [];

        /// <summary>
        /// The validator rules for the entity.
        /// </summary>
        private ValidatorRules<Entity<TEntity, TProps>> _validatorRules = new();

        /// <summary>
        /// The broken rules of the entity.
        /// </summary>
        private BrokenRules _brokenRules = new();

        /// <summary>
        /// The properties of the entity.
        /// </summary>
        private TProps _props;

        /// <summary>
        /// The version of the entity.
        /// </summary>
        private int _version;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tracking state of the entity.
        /// </summary>
        public Tracking Tracking { get; private set; } = Tracking.MarkClean();

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
        /// Gets a value indicating whether the entity is self deleted.
        /// </summary>
        public bool IsSelftDeleted => Tracking.IsSelftDeleted;

        /// <summary>
        /// Gets a value indicating whether the entity is deleted.
        /// </summary>
        public bool IsDeleted => Tracking.IsDeleted;

        /// <summary>
        /// Gets or sets the version of the entity.
        /// </summary>
        public int Version
        {
            get { return _version; }
            private set { _version = value; }
        }

        #endregion

        #region Constructors

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

        public virtual void SelfDelete()
        {
            MarkSelfDeleted();
        }

        public virtual void Delete()
        {
            MarkDelete();
        }

        /// <summary>
        /// Gets the validator rules for the entity.
        /// </summary>
        public ReadOnlyCollection<AbstractRuleValidator<Entity<TEntity, TProps>>> GetValidators() => _validatorRules.GetValidators();

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

            Tracking = Tracking.MarkDirty();
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

        /// <summary>
        /// Marks the entity as dirty.
        /// </summary>
        public void MarkDirty()
        {
            Tracking = Tracking.MarkDirty();
        }

        /// <summary>
        /// Marks the entity as new.
        /// </summary>
        public void MarkNew()
        {
            Tracking = Tracking.MarkNew();
        }

        /// <summary>
        /// Marks the entity as self deleted.
        /// </summary>
        public void MarkSelfDeleted() 
        {
            Tracking = Tracking.MarkSelfDeleted();
        } 

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        public void MarkDelete()
        {
            Tracking = Tracking.MarkDeleted();
        }

        #endregion

        #region DomainEvents                        

        /// <summary>
        /// Gets the domain events associated with the entity.
        /// </summary>
        /// <returns>The domain events associated with the entity.</returns>
        public IReadOnlyCollection<DomainEvent> GetDomainEvents()
        {            
            return _domainEvents.ToList().AsReadOnly();
        }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(DomainEvent eventItem)
        {
            if (!_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any())
            {
                _domainEvents.Add(eventItem);
            }
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(DomainEvent eventItem)
        {
            if (_domainEvents.Where(p => p.EventName.ToUpperInvariant().Trim() == eventItem.EventName.ToUpperInvariant().Trim()).Any()) 
            { 
                _domainEvents?.Remove(eventItem);
            }
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
                MarkDirty();
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
            if (!_validatorRules.GetValidators().Any(p => p.ValidatorName.ToUpperInvariant().Trim() == validator.ValidatorName.ToUpperInvariant().Trim()))
            {
                _validatorRules.Add(validator);
            }
        }

        /// <summary>
        /// Adds multiple validators for the entity.
        /// </summary>
        /// <param name="validators">The validators to add.</param>
        public void AddValidators(ICollection<AbstractRuleValidator<Entity<TEntity, TProps>>> validators)
        {
            validators.ToList().ForEach(i => AddValidator(i));
        }

        /// <summary>
        /// Removes a validator from the entity.
        /// </summary>
        /// <param name="validator">The validator to remove.</param>
        public void RemoveValidator(AbstractRuleValidator<Entity<TEntity, TProps>> validator)
        {
            if (_validatorRules.GetValidators().Any(p => p.ValidatorName.ToUpperInvariant().Trim() == validator.ValidatorName.ToUpperInvariant().Trim()))
            {
                _validatorRules.Remove(validator);
            }
        }

        /// <summary>
        /// Gets the broken rules of the entity.
        /// </summary>
        /// <returns>The broken rules of the entity.</returns>
        public ReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            return _brokenRules.GetBrokenRules();
        }

        /// <summary>
        /// Gets the broken rules of the entity as a string.
        /// </summary>
        /// <returns>The broken rules of the entity as a string.</returns>
        public string GetBrokenRulesAsString()
        {
            if (!_brokenRules.GetBrokenRules().Any()) return string.Empty;

            var sb = new StringBuilder();

            foreach (var rule in _brokenRules.GetBrokenRules())
            {
                var line = $"Property: {rule.Property}, Message: {rule.Message}";

                sb.AppendLine(line);
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
            var brokenRule = new BrokenRule(propertyName, message);

            if (!_brokenRules.GetBrokenRules().Any(x => x.Property.ToUpperInvariant() == brokenRule.Property.ToUpperInvariant()
                                                        && x.Message.ToUpperInvariant() == brokenRule.Message.ToUpperInvariant()))
            {
                _brokenRules.Add(brokenRule);
            }

            _brokenRules.Add(brokenRule);
        }

        /// TODO: How improve this method?, how reduce reflextion?
        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Entity<TEntity, TProps>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            return ReferenceEntityPropertiesEquals(obj);
        }

        // TODO: An entity can have ValueObjects and Primitive objects. How support this?   
        private bool ReferenceEntityPropertiesEquals(object? obj)
        {
            if (obj is not Entity<TEntity, TProps> entity)
                return false;
            
            var props = GetPropsCopy();
            var propsOthers = entity.GetPropsCopy();

            if (props == null || propsOthers == null)
                return false;

            var propsId= props.GetType().GetProperty("Id");
            var propsOthersId = propsOthers.GetType().GetProperty("Id");

            if (propsId == null || propsOthersId == null)
                return false;

            var propsIdValue = propsId.GetValue(props);
            var propsOthersIdValue = propsOthersId.GetValue(propsOthers);

            if (!propsIdValue!.Equals(propsOthersIdValue))
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

