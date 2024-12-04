using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Addresses;
using Core.Entities;

namespace Core.AutoMapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressReturnDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))
           .ReverseMap();

            CreateMap<Address, AddressFullReturnDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))
            .ForMember(x => x.Client, opt => opt.MapFrom(o => o.Client))
            .ReverseMap();

            CreateMap<AddressCreateDTO, Address>()
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))
            ;

            CreateMap<AddressUpdateDTO, AddressCreateDTO>()
            .ReverseMap();

            CreateMap<AddressUpdateDTO, Address>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))
            .ReverseMap();

            CreateMap<Address, Address>()
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))
            .ForMember(x => x.CreatedAt, opt => opt.UseDestinationValue())
            .ForMember(x => x.IsDeleted, opt => opt.UseDestinationValue())
            ;
        }
    }
}