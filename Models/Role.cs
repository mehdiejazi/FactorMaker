using Models.Base;
using System.Collections.Generic;

namespace Models
{
    public class Role : EntityBase
    {
        public string RolName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Action> ActionPermissions { get; set; }
    }
}
