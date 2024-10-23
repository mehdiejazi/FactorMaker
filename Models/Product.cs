using Models.Base;
using System;

namespace Models
{
    public class Product : EntityBase
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
