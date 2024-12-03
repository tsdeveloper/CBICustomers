using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Client : IdentityUser, IBaseEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}