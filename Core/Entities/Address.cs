namespace Core.Entities
{
    public class Address : BaseEntity
    {
        public string Name { get; set; }
        public Guid ClientId { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Client Client { get; set; }
       
    }
}