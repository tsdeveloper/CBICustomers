using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Addresses;

namespace Core.DTOs.Clients
{
    public class ClientUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressUpdateDTO? Address { get; set; }

    }

    public class ClientCreateDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public AddressCreateDTO Address { get; set; }

    }

    public class ClientReturnDTO
    {
          public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public AddressReturnDTO Address { get; set; }

    }

    public class ClientFullReturnDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressReturnDTO Address { get; set; }
    }

    public class ClientLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ClientRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ClientReturnRegisterDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }

    public class ClientEditRegisterDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressReturnDTO Address { get; set; }

    }
}