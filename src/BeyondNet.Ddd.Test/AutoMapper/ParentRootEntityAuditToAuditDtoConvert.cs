using AutoMapper;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.Test.Entities.ValueObjects;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootEntityAuditToAuditDtoConvert : ITypeConverter<Audit, AuditDto>
    {
        public AuditDto Convert(Audit source, AuditDto destination, ResolutionContext context)
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
