using Models.Base;
using System.Collections.Generic;

namespace Models
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RoleActionPermission> RoleActionPermissions { get; set; }
        public bool IsDefault { get; set; }
    }
}
