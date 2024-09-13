using AutoMapper;
using BeyondNet.Ddd.AutoMapper.Impl;
using BeyondNet.Ddd.Test.Stubs;
using BeyondNet.Ddd.ValueObjects;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootProfile : Profile
    {
        public ParentRootProfile()
        {
            CreateMap<ParentRootProps, ParentRootDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.Audit, opt => opt.MapFrom(src => src.Audit));

            CreateMap<AuditDto, Audit>().ConvertUsing<ParentRootAuditDtoToEntityConvert>();
            CreateMap<Audit, AuditDto>().ConvertUsing<ParentRootEntityAuditToAuditDtoConvert>();
            CreateMap<int, ParentRootEntityStatus>().ConvertUsing<EnumerationConverter<ParentRootEntityStatus>>();

            CreateMap<ParentRootDto, ParentRootProps>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Audit, opt => opt.MapFrom(src => src.Audit));






            //.ForMember(dest => dest.Status, opt => opt.Ignore())
            //.ForMember(dest => dest.Audit, opt => opt.Ignore());
            //.ForMember(dest => dest.Audit.CreatedBy, opt => opt.MapFrom(src => src.GetPropsCopy().Audit.GetValue().CreatedBy))
            //.ForMember(dest => dest.Audit.CreatedDate, opt => opt.MapFrom(src => src.GetPropsCopy().Audit.GetValue().CreatedAt))
            //.ForMember(dest => dest.Audit.UpdatedBy, opt => opt.MapFrom(src => src.GetPropsCopy().Audit.GetValue().UpdatedBy))
            //.ForMember(dest => dest.Audit.UpdatedDate, opt => opt.MapFrom(src => src.GetPropsCopy().Audit.GetValue().UpdatedAt))
            //.ForMember(dest => dest.Audit.TimeSpan, opt => opt.MapFrom(src => src.GetPropsCopy().Audit.GetValue().TimeSpan));
        }
    }
}
