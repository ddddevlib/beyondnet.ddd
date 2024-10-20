using AutoMapper;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.Test.Entities.ValueObjects;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootAuditDtoToEntityConvert : ITypeConverter<AuditDto, Audit>
    {
        public Audit Convert(AuditDto source, Audit destination, ResolutionContext context)
        {
            var props = new AuditProps() {
                CreatedBy = source.CreatedBy,
                CreatedAt = source.CreatedAt,
                UpdatedBy = source.UpdatedBy,
                UpdatedAt = source.UpdatedAt,
                TimeSpan = source.TimeSpan.ToString()
            };

            var audit = Audit.Load(props);

            return audit;
        }
    }
}
