namespace BeyondNet.Ddd.Test.Entities.ValueObjects
{
    public struct AuditProps
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TimeSpan { get; set; }

        override public string ToString()
        {
            return $"CreatedBy: {CreatedBy}, CreatedAt: {CreatedAt}, UpdatedBy: {UpdatedBy}, UpdatedAt: {UpdatedAt}, TimeSpan: {TimeSpan}";
        }
    }

    public class Audit : ValueObject<AuditProps>
    {
        private Audit(AuditProps value) : base(value)
        {
        }

        public static Audit Create(string createdBy)
        {
            return new Audit(new AuditProps
            {
                CreatedBy = createdBy,
                CreatedAt = DateTime.Today.ToUniversalTime(),
                TimeSpan = TimeSpan.FromTicks(DateTime.Today.Ticks).ToString(),
            });
        }

        public static Audit Load(AuditProps props)
        {
            return new Audit(props);
        }

        public void Update(string updatedBy)
        {
            SetValue(new AuditProps
            {
                CreatedBy = GetValue().CreatedBy,
                CreatedAt = GetValue().CreatedAt,
                UpdatedBy = updatedBy,
                UpdatedAt = DateTime.Today.ToUniversalTime(),
                TimeSpan = TimeSpan.FromTicks(DateTime.Today.Ticks).ToString(),
            });
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return GetValue().CreatedBy;
            yield return GetValue().CreatedAt;
            yield return GetValue().UpdatedBy!;
            yield return GetValue().UpdatedAt!;
            yield return GetValue().TimeSpan;
        }
    }
}
