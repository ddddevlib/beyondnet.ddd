using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.Rules.Impl;
using BeyondNet.Ddd.Services.Impl;

namespace BeyondNet.Ddd.Interfaces
{
    public interface IEntity<TEntity, TProps>
        where TEntity : class
        where TProps : class, IProps
    {
        BrokenRulesManager BrokenRules { get; }
        IdValueObject Id { get; }
        TProps Props { get; }
        TrackingStateManager TrackingState { get; }
        ValidatorRuleManager<AbstractRuleValidator<TEntity>> ValidatorRules { get;  }
        void AddValidators();
        bool Equals(object? obj);
        int GetHashCode();
        TProps GetPropsCopy();
        bool IsValid();
        IdValueObject SetId(string id);
        void SetProps(TProps props);
        void Validate();
    }
}