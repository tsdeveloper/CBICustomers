using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Client : IdentityUser, IBaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public ICollection<Address> AddressList { get; set; } = new List<Address>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}