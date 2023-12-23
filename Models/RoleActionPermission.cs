using Models.Base;
using System;

namespace Models
{
    public class RoleActionPermission: EntityBase
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid ActionPermissionId { get; set; }
        public ActionPermission ActionPermission { get; set; }
    }
}
