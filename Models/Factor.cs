using Models.Base;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Factor : EntityBase
    {
        public virtual Customer Owner { get; set; }
        public Guid OwnerId { get; set; }
        public virtual Store Store { get; set; }
        public Guid StoreId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FactorItem> FactorItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsClosed { get; set; }

    }
}
