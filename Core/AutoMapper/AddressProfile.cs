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
            .ForMember(x => x.Client, opt => opt.MapFrom(o => o.Client))            
           .ReverseMap();

            CreateMap<Address, AddressFullReturnDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))            
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))            
            .ForMember(x => x.Client, opt => opt.MapFrom(o => o.Client))            
            .ReverseMap();  

            CreateMap<Address, AddressCreateDTO>()
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))      
            ;      

            CreateMap<Address, AddressUpdateDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))            
            .ForMember(x => x.ClientId, opt => opt.MapFrom(o => o.ClientId))            
            .ReverseMap();                    
        }
    }
}