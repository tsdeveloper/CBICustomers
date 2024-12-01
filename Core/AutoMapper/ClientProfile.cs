using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.Clients;
using Core.Entities;

namespace Core.AutoMapper
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientReturnDTO>()
           .ForMember(x => x.CreatedAt, opt => opt.MapFrom(o => o.CreatedAt))
           .ReverseMap();

            CreateMap<Client, ClientFullReturnDTO>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(o => o.CreatedAt))
            .ForMember(x => x.AddressList, opt => opt.MapFrom(o => o.AddressList))
            .ReverseMap();  

            CreateMap<Client, ClientCreateDTO>()
            .ForMember(x => x.AddressList, opt => opt.MapFrom(o => o.AddressList))
            .ReverseMap();

            CreateMap<Client, ClientUpdateDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.AddressList, opt => opt.MapFrom(o => o.AddressList))
            .ReverseMap();                    
        }
    }
}