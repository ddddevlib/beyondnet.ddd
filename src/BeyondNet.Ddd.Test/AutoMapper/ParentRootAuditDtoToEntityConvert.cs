using AutoMapper;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.ValueObjects.Audit;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootAuditDtoToEntityConvert : ITypeConverter<AuditDto, AuditValueObject>
    {
        public AuditValueObject Convert(AuditDto source, AuditValueObject destination, ResolutionContext context)
        {
            var props = new AuditProps() {
                CreatedBy = source.CreatedBy,
                CreatedAt = source.CreatedAt,
                UpdatedBy = source.UpdatedBy,
                UpdatedAt = source.UpdatedAt,
                TimeSpan = source.TimeSpan.ToString()
            };

            var audit = AuditValueObject.Load(props);

            return audit;
        }
    }
}
