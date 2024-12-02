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
            .ForMember(x => x.Address, opt => opt.MapFrom(o => o.Address))
            .ReverseMap();  

            CreateMap<Client, ClientCreateDTO>()
            .ForMember(x => x.Address, opt => opt.MapFrom(o => o.Address))
            .ReverseMap();

            CreateMap<Client, ClientUpdateDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
            .ForMember(x => x.Address, opt => opt.MapFrom(o => o.Address))
            .ReverseMap();     

            CreateMap<ClientRegisterDto, Client >()
           .ForMember(x => x.Name, opt => opt.MapFrom(o => o.DisplayName))
           .ForMember(x => x.Email, opt => opt.MapFrom(o => o.Email))
           .ForMember(x => x.UserName, opt => opt.MapFrom(o => o.Email))
           .ReverseMap();  

            CreateMap<Client, ClientReturnRegisterDto>()
           .ForMember(x => x.DisplayName, opt => opt.MapFrom(o => o.Name))
           .ForMember(x => x.Email, opt => opt.MapFrom(o => o.Email))
           .ReverseMap();              
        }
    }
}