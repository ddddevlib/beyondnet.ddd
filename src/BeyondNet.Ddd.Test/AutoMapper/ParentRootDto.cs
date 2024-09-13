namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public AuditDto Audit { get; set; }

    }

    public class AuditDto
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string TimeSpan { get; set; }
    }
}
