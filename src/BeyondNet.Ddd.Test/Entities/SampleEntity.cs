namespace BeyondNet.Ddd.Test.Entities
{
    public class SampleEntityProps: IProps
    {
        public IdValueObject Id { get; private set; } = default!;
        public SampleName Name { get; private set; }
        public SampleReferenceId SampleReferenceId { get; set; }
        public SampleEntityStatus Status { get; set; }
        public AuditValueObject Audit { get; private set; }


        public SampleEntityProps(IdValueObject id, SampleName name, SampleReferenceId sampleReferenceId)
        {
            Id = id;
            Name = name;
            SampleReferenceId = sampleReferenceId;
            Status = SampleEntityStatus.Active;
            Audit = AuditValueObject.Create("default");
        }

        public SampleEntityProps(IdValueObject id, SampleName name, SampleReferenceId sampleReferenceId, SampleEntityStatus status, AuditValueObject audit)
        {
            Id = id;
            SampleReferenceId = sampleReferenceId;
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

        public static SampleEntity Create(IdValueObject id, SampleName name, SampleReferenceId sampleReferenceId)
        {
            var props = new SampleEntityProps(id, name, sampleReferenceId);

            return new SampleEntity(props);
        }

        public static SampleEntity Load(SampleEntityProps props)
        {
            return new SampleEntity(props);
        }

        public void ChangeSampleReference(SampleReferenceId sampleReferenceId)
        {
            Props.SampleReferenceId = sampleReferenceId;
            Props.Audit.Update("default");
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
            AddValidator(new SampleEntityValidator(this));
        }

        protected override void ChangeStateDomainEvents(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
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
