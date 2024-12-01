using System;
using Core.Entities;

namespace Core.Specification.Clients.SpecParams
{
    public class AddressSpecParams : BaseSpecParams
    {
        public string? ZipCode { get;  set; }
        public string? City { get;  set; }
        public string? State { get;  set; }
    }
}