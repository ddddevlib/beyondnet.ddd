using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Services.Impl;
using BeyondNet.Ddd.Services.Interfaces;

namespace BeyondNet.Ddd.Interfaces
{
    /// <summary>
    /// Represents an entity in the domain.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProps">The type of the entity properties.</typeparam>
    public interface IEntity<TEntity, TProps> 
            where TEntity : class
            where TProps : class, IProps
    {
        BrokenRulesManager GetBrokenRules { get; }
        IdValueObject Id { get; }
        bool IsDeleted { get; }
        bool IsDirty { get; }
        bool IsNew { get; }
        bool IsSelftDeleted { get; }
        TProps Props { get; }
        TrackingManager Tracking { get; }

        void AddBrokenRule(string propertyName, string message);
        void AddValidator(AbstractRuleValidator<TEntity> validator);
        void AddValidators();
        void AddValidators(ICollection<AbstractRuleValidator<TEntity>> validators);
        void Delete();
        bool Equals(object? obj);
        int GetHashCode();
        TProps GetPropsCopy();
        ReadOnlyCollection<AbstractRuleValidator<TEntity>> GetValidators();
        bool IsValid();
        void MarkDelete();
        void MarkDirty();
        void MarkNew();
        void MarkSelfDeleted();
        void RemoveValidator(AbstractRuleValidator<TEntity> validator);
        void SelfDelete();
        IdValueObject SetId(string id);
        void SetProps(TProps props);
        void Validate();

    }
}
