using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Clients;
using Core.Entities;

namespace Core.DTOs.Addresses
{
    public class AddressReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }

    public class AddressFullReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public ClientReturnDTO Client { get; set; }
    }

    public class AddressUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }

    public class AddressCreateDTO
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}