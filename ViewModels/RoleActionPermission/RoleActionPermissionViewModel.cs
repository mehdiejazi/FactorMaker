using System;
using ViewModels.ActionPermission;
using ViewModels.Base;
using ViewModels.Role;

namespace ViewModels.RoleActionPermission
{
    public class RoleActionPermissionViewModel : ViewModelBase
    {
        public Guid RoleId { get; set; }
        public RoleViewModel Role { get; set; }
        public Guid ActionPermissionId { get; set; }
        public ActionPermissionViewModel ActionPermission { get; set; }
    }
}
