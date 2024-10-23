using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Extensions;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd
{
    /// <summary>
    /// Represents an abstract base class for entities in the domain-driven design.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProps">The type of the entity properties.</typeparam>
    public abstract class Entity<TEntity, TProps> : IEntity<TEntity, TProps> where TEntity : class
            where TProps : class, IProps
    {
        #region Members         

        /// <summary>
        /// The properties of the entity.
        /// </summary>
        private TProps _props;

        #endregion

        #region Properties 

        public IdValueObject Id { get; private set; }

        public IdValueObject SetId(string id)
        {
            return IdValueObject.Create(id);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TEntity, TProps}"/> class.
        /// </summary>
        /// <param name="props">The properties of the entity.</param>
        protected Entity(TProps props)
        {
            Id = IdValueObject.Create();

            _brokenRules = new BrokenRulesManager();

            _props = props;

            Validate();

            Tracking = TrackingManager.MarkNew();
        }

        #endregion

        #region Methods

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

            Tracking = TrackingManager.MarkDirty();
        }

        #endregion

        #region Tracking     

        public virtual void SelfDelete()
        {
            MarkSelfDeleted();
        }

        public virtual void Delete()
        {
            MarkDelete();
        }

        /// <summary>
        /// Marks the entity as dirty.
        /// </summary>
        public void MarkDirty()
        {
            Tracking = TrackingManager.MarkDirty();
        }

        /// <summary>
        /// Marks the entity as new.
        /// </summary>
        public void MarkNew()
        {
            Tracking = TrackingManager.MarkNew();
        }

        /// <summary>
        /// Marks the entity as self deleted.
        /// </summary>
        public void MarkSelfDeleted()
        {
            Tracking = TrackingManager.MarkSelfDeleted();
        }

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        public void MarkDelete()
        {
            Tracking = TrackingManager.MarkDeleted();
        }

        /// <summary>
        /// Gets or sets the tracking state of the entity.
        /// </summary>
        public TrackingManager Tracking { get; private set; } = TrackingManager.MarkClean();

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

        #endregion  

        #region BrokenRules

        /// <summary>
        /// The broken rules of the entity.
        /// </summary>
        private BrokenRulesManager _brokenRules = new();

        /// <summary>
        /// Gets the broken rules of the entity.
        /// </summary>
        /// <returns>The broken rules of the entity.</returns>
        public BrokenRulesManager GetBrokenRules => _brokenRules;

        /// <summary>
        /// Adds a broken rule to the entity.
        /// </summary>
        /// <param name="propertyName">The name of the property with the broken rule.</param>
        /// <param name="message">The message of the broken rule.</param>
        public void AddBrokenRule(string propertyName, string message)
        {
            var brokenRule = new BrokenRule(propertyName, message);
            _brokenRules.Add(brokenRule);
        }

        #endregion

        #region ValidatorRules

        /// <summary>
        /// Gets the validator rules for the entity.
        /// </summary>
        public ReadOnlyCollection<AbstractRuleValidator<TEntity>> GetValidators() => _validatorRules.GetValidators().AsReadOnly();

        /// <summary>
        /// The validator rules for the entity.
        /// </summary>
        private ValidatorRuleManager<AbstractRuleValidator<TEntity>> _validatorRules = new();

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
        /// Adds the validators for the value object.
        /// </summary>
        public virtual void AddValidators()
        {

        }

        /// <summary>
        /// Adds a validator for the entity.
        /// </summary>
        /// <param name="validator">The validator to add.</param>
        public void AddValidator(AbstractRuleValidator<TEntity> validator)
        {
            _validatorRules.Add(validator);
        }

        /// <summary>
        /// Adds multiple validators for the entity.
        /// </summary>
        /// <param name="validators">The validators to add.</param>
        public void AddValidators(ICollection<AbstractRuleValidator<TEntity>> validators)
        {
            _validatorRules.Add(validators);
        }

        /// <summary>
        /// Removes a validator from the entity.
        /// </summary>
        /// <param name="validator">The validator to remove.</param>
        public void RemoveValidator(AbstractRuleValidator<TEntity> validator)
        {
            _validatorRules.Remove(validator);
        }



        #endregion

        #region Equality

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

            var propsId = this.GetType().GetProperty("Id");
            var propsOthersId = obj.GetType().GetProperty("Id");

            if (propsId == null || propsOthersId == null)
                return false;

            var propsIdValue = propsId.GetValue(this);
            var propsOthersIdValue = propsOthersId.GetValue(obj);

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

