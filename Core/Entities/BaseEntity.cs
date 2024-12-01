using System;

namespace Core.Entities
{
    public class BaseEntity:   IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

public interface IBaseEntity
{
  
}