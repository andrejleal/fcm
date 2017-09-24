using AutoMapper;
using FCM.API.REST.Contracts;
using FCM.Application.Contracts;
using FCM.DomainModel.Entities;

namespace FCM.API.REST.Providers
{
    public class AutoMapperMappingProfile : Profile
    {
        public AutoMapperMappingProfile()
        {
            CreateMap<ComponentModel, ComponentDTO>().ReverseMap();
            CreateMap<ComponentPropertyModel, ComponentPropertyDTO>().ReverseMap();
            CreateMap<ExternalSystemModel, ExternalSystemDTO>();
            CreateMap<ExternalSystemOutputDTO, ExternalSystemResponseModel>();

            CreateMap<ComponentDTO, Component>().ReverseMap();
            CreateMap<ComponentPropertyDTO, ComponentProperty>().ReverseMap();

            CreateMap<ExternalSystemDTO, ExternalSystem>();
            CreateMap<ExternalSystem, ExternalSystemOutputDTO>();
        }

    }
}
