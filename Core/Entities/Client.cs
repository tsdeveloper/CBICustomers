using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Client : IdentityUser, IBaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public ICollection<Address> AddressList { get; set; } = new List<Address>();
        public string? Logo { get; set; }
    }
}