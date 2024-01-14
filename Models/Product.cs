using Models.Base;
using System;

namespace Models
{
    public class Product : EntityBase
    {
        public User Owner;
        public Guid OwnerId;
        public string Name { get; set; }
        public long Price { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
