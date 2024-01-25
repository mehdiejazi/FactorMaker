using Models.Base;
using System;

namespace Models
{
    public class Category : EntityBase
    {
        public Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
    }
}
