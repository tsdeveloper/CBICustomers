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
           .ForMember(x => x.Phone, opt => opt.MapFrom(o => o.PhoneNumber))
           .ReverseMap();

            CreateMap<Client, ClientFullReturnDTO>()
            .ForMember(x => x.Phone, opt => opt.MapFrom(o => o.PhoneNumber))
            .ForMember(x => x.AddressList, opt => opt.MapFrom(o => o.AddressList))
            .ReverseMap();

            CreateMap<Client, ClientCreateDTO>()
            .ReverseMap();

            CreateMap<ClientUpdateDTO, Client>()
            .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(o => o.Phone))
            .ForMember(x => x.UserName, opt => opt.MapFrom(o => o.Email))
            .ForMember(x => x.Email, opt => opt.MapFrom(o => o.Email))
            .ForMember(x => x.PasswordHash, opt => opt.UseDestinationValue())
            .ForMember(x => x.CreatedAt, opt => opt.UseDestinationValue())
            .ForMember(x => x.EmailConfirmed, opt => opt.UseDestinationValue())
            .ForMember(x => x.PhoneNumberConfirmed, opt => opt.UseDestinationValue())
            .ForMember(x => x.AccessFailedCount, opt => opt.UseDestinationValue())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.UseDestinationValue())
            .ForMember(x => x.LockoutEnabled, opt => opt.UseDestinationValue())
            .ForMember(x => x.LockoutEnd, opt => opt.UseDestinationValue())
            .ForMember(x => x.NormalizedUserName, opt => opt.UseDestinationValue())
            .ForMember(x => x.SecurityStamp, opt => opt.UseDestinationValue())
            .ForMember(x => x.TwoFactorEnabled, opt => opt.UseDestinationValue())
            .ReverseMap();

            CreateMap<ClientRegisterDto, Client>()
           .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
           .ForMember(x => x.Email, opt => opt.MapFrom(o => o.Email))
           .ForMember(x => x.UserName, opt => opt.MapFrom(o => o.Email))
           .ReverseMap();

            CreateMap<Client, ClientReturnRegisterDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Id))
           .ForMember(x => x.Name, opt => opt.MapFrom(o => o.Name))
           .ForMember(x => x.Email, opt => opt.MapFrom(o => o.Email))
           .ReverseMap();
        }
    }
}