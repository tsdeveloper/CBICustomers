using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.Addresses;

namespace Core.DTOs.Clients
{
    public class ClientUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public ICollection<ClientUpdateDTO> AddressList { get; set; } = new List<ClientUpdateDTO>();

    }

    public class ClientCreateDTO
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public ICollection<ClientCreateDTO> AddressList { get; set; } = new List<ClientCreateDTO>();

    }

    public class ClientReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<ClientCreateDTO> AddressList { get; set; } = new List<ClientCreateDTO>();

    }

    public class ClientFullReturnDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Phone { get; set; }
        public IReadOnlyList<AddressReturnDTO> AddressList { get; set; } = new List<AddressReturnDTO>();
    }
}