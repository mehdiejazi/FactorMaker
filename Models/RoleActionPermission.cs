using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RoleActionPermission
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid ActionPermissionId { get; set; }
        public ActionPermission ActionPermission { get; set; }
    }
}
