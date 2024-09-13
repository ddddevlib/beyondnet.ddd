using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.Test.Stubs
{
    public class ParentRootProps: IProps
    {
        public IdValueObject Id { get; private set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public ParentRootEntityStatus Status { get; private set; }
        public Audit Audit { get; private set; }


        public ParentRootProps(Name name, Description description)
        {
            Id = IdValueObject.Create();
            Name = name;
            Description = description;
            Status = ParentRootEntityStatus.Active;
            Audit = Audit.Create("default");
        }

        public object Clone()
        {
            return new ParentRootProps(Name, Description);
        }
    }

    public class ParentRootEntity : Entity<ParentRootEntity, ParentRootProps>, IAggregateRoot
    {
        private ParentRootEntity(ParentRootProps props) : base(props)
        {
        }

        public static ParentRootEntity Create(Name name,Description description)
        {
            var props = new ParentRootProps(name,description);

            return new ParentRootEntity(props);
        }

        public void ChangeName(Name name)
        {
            GetProps().Name.SetValue(name.GetValue());
            var x = GetBrokenRules();

            GetProps().Audit.Update("default");
        }

        public void ChangeDescription(Description description)
        {
            GetProps().Description.SetValue(description.GetValue());

            GetProps().Audit.Update("default");
        }

        public void Inactivate()
        {
            if (GetPropsCopy().Status == ParentRootEntityStatus.Inactive)
            {
                AddBrokenRule("Status", "The entity is already inactive");
                return;
            }

            GetProps().Status.SetValue<ParentRootEntityStatus>(ParentRootEntityStatus.Inactive.Id);
            GetProps().Audit.Update("default");
        }

        public void Activate()
        {
            if (GetPropsCopy().Status == ParentRootEntityStatus.Active)
            {
                AddBrokenRule("Status", "The entity is already active");
                return;
            }

            GetProps().Status.SetValue<ParentRootEntityStatus>(ParentRootEntityStatus.Active.Id);
            GetProps().Audit.Update("default");
        }
    }

    public class ParentRootEntityStatus : Enumeration
    {
        public static ParentRootEntityStatus Active = new ParentRootEntityStatus(1, "Active");
        public static ParentRootEntityStatus Inactive = new ParentRootEntityStatus(2, "Inactive");

        public ParentRootEntityStatus(int id, string name) : base(id, name)
        {
        }
    }
}
