using AutoMapper;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.ValueObjects.Audit;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootEntityAuditToAuditDtoConvert : ITypeConverter<AuditValueObject, AuditDto>
    {
        public AuditDto Convert(AuditValueObject source, AuditDto destination, ResolutionContext context)
        {
            var dto = new AuditDto
            {
                CreatedBy = source.GetValue().CreatedBy,
                CreatedAt = source.GetValue().CreatedAt,
                UpdatedBy = source.GetValue().UpdatedBy!,
                UpdatedAt = source.GetValue().UpdatedAt,
                TimeSpan = source.GetValue().TimeSpan
            };

            return dto;
        }
    }
}
