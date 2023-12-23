using Models.Base;
using System.Collections.Generic;

namespace Models
{
    public class ActionPermission:EntityBase
    {
        public string ControllerName { get; set; }
        public string  ActionName { get; set; }
        public string Url { get; set; }
        public virtual ICollection<RoleActionPermission> RoleActionPermission { get; set; }

    }
}
