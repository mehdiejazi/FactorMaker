using Models.Base;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Factor : EntityBase
    {
        public Customer Owner { get; set; }
        public Guid OwnerId { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<FactorItem> FactorItems { get; set; }

    }
}
