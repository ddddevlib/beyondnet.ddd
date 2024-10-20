using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Test.Entities.ValueObjects;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.Test.Entities
{
    public class SampleEntityProps: IProps
    {
        public IdValueObject Id { get; private set; } = default!;
        public SampleName Name { get; private set; }
        public SampleEntityStatus Status { get; set; }
        public Audit Audit { get; private set; }


        public SampleEntityProps(SampleName name)
        {
            Name = name;
            Status = SampleEntityStatus.Active;
            Audit = Audit.Create("default");
        }

        public SampleEntityProps(IdValueObject id, SampleName name, SampleEntityStatus status, Audit audit)
        {
            Id = id;
            Name = name;
            Status = status;
            Audit = audit;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SampleEntity : Entity<SampleEntity, SampleEntityProps>, IAggregateRoot
    {
        private SampleEntity(SampleEntityProps props) : base(props)
        {
        }

        public static SampleEntity Create(SampleName name)
        {
            var props = new SampleEntityProps(name);

            return new SampleEntity(props);
        }

        public static SampleEntity Load(SampleEntityProps props)
        {
            return new SampleEntity(props);
        }

  
        public void ChangeName(StringValueObject name)
        {
            Props.Name.SetValue(name.GetValue());
            Props.Audit.Update("default");
        }

        public void Inactivate()
        {
            if (GetPropsCopy().Status == SampleEntityStatus.Inactive)
            {
                AddBrokenRule("Status", "The entity is already inactive");
                return;
            }

            Props.Status = SampleEntityStatus.Inactive;
            Props.Audit.Update("default");
        }

        public void Activate()
        {
            if (GetPropsCopy().Status == SampleEntityStatus.Active)
            {
                AddBrokenRule("Status", "The entity is already active");
                return;
            }

            Props.Status = SampleEntityStatus.Active;
            Props.Audit.Update("default");
        }

        public override void AddValidators()
        {
            
        }
    }

    public class SampleEntityStatus : Enumeration
    {
        public static SampleEntityStatus Active = new SampleEntityStatus(1, "Active");
        public static SampleEntityStatus Inactive = new SampleEntityStatus(2, "Inactive");

        public SampleEntityStatus(int id, string name) : base(id, name)
        {
        }
    }
}
