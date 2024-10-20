using AutoMapper;
using BeyondNet.Ddd.AutoMapper.Impl;
using BeyondNet.Ddd.Test.Dtos;
using BeyondNet.Ddd.Test.Entities;
using BeyondNet.Ddd.Test.Entities.ValueObjects;

namespace BeyondNet.Ddd.Test.AutoMapper
{
    public class ParentRootProfile : Profile
    {
        public ParentRootProfile()
        {
            CreateMap<AuditDto, Audit>().ConvertUsing<ParentRootAuditDtoToEntityConvert>();
            CreateMap<Audit, AuditDto>().ConvertUsing<ParentRootEntityAuditToAuditDtoConvert>();
            CreateMap<int, SampleEntityStatus>().ConvertUsing<EnumerationConverter<SampleEntityStatus>>();

            CreateMap<SampleEntityProps, SampleEntityDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<SampleEntityDto, SampleEntityProps>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
