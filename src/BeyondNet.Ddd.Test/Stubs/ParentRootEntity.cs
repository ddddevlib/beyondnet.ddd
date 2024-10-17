using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.Test.Stubs
{
    public class ParentRootProps: IProps
    {
        public IdValueObject Id { get; private set; }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public ComplexityLevelEnum ComplexityLevel { get; private set; }
        public ParentRootEntityStatus Status { get; set; }
        public Audit Audit { get; private set; }


        public ParentRootProps(Name name, Description description)
        {
            Id = IdValueObject.Create();
            Name = name;
            Description = description;
            Status = ParentRootEntityStatus.Active;
            ComplexityLevel = ComplexityLevelEnum.Low;
            Audit = Audit.Create("default");
        }

        public ParentRootProps(IdValueObject id, Name name, Description description)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = ParentRootEntityStatus.Active;
            ComplexityLevel = ComplexityLevelEnum.Low;
            Audit = Audit.Create("default");
        }

        public object Clone()
        {
            return new ParentRootProps(Id, Name, Description);
        }
    }

    public class ParentRootEntity : Entity<ParentRootEntity, ParentRootProps>, IAggregateRoot
    {
        private ParentRootEntity(ParentRootProps props) : base(props)
        {
        }

        public static ParentRootEntity Create(IdValueObject id, Name name, Description description)
        {
            var props = new ParentRootProps(id, name, description);

            return new ParentRootEntity(props);
        }

        public static ParentRootEntity Create(Name name,Description description)
        {
            var props = new ParentRootProps(name,description);

            return new ParentRootEntity(props);
        }

  
        public void ChangeName(Name name)
        {
            Props.Name.SetValue(name.GetValue());
            Props.Audit.Update("default");
        }

        public void ChangeDescription(Description description)
        {
            Props.Description.SetValue( description.GetValue());
            Props.Audit.Update("default");
        }

        public void Inactivate()
        {
            if (GetPropsCopy().Status == ParentRootEntityStatus.Inactive)
            {
                AddBrokenRule("Status", "The entity is already inactive");
                return;
            }

            Props.Status = ParentRootEntityStatus.Inactive;
            Props.Audit.Update("default");
        }

        public void Activate()
        {
            if (GetPropsCopy().Status == ParentRootEntityStatus.Active)
            {
                AddBrokenRule("Status", "The entity is already active");
                return;
            }

            Props.Status = ParentRootEntityStatus.Active;
            Props.Audit.Update("default");
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
