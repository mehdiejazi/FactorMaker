using Models.Base;
using System;

namespace Models
{
    public class Category : EntityBase
    {
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
    }
}
