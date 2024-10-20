using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Test.Entities
{
    public class SampleReferenceIdProps : IProps
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public SampleReferenceIdProps(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class SampleReferenceId : ValueObject<SampleReferenceIdProps>
    {
        public SampleReferenceId(SampleReferenceIdProps value) : base(value)
        {
        }

        public static SampleReferenceId Create(string id, string name)
        {
            return new SampleReferenceId(new SampleReferenceIdProps(id, name));
        }

        public override void AddValidators()
        {
            base.AddValidators();

            AddValidator(new SampleReferenceIdValidator(this));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue().Id;
            yield return GetValue().Name;
        }
    }
}
