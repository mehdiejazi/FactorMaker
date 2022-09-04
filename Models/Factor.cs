using Models.Base;
using System.Collections.Generic;

namespace Models
{
    public class Factor : EntityBase
    {
        public Customer Owner { get; set; }
        public User Creator { get; set; }
        public virtual ICollection<FactorItem> FactorItems { get; set; }

    }
}
