using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.RoleActionPermission;

namespace ViewModels.Role
{
    public class RoleViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RoleActionPermissionViewModel> RoleActionPermissions { get; set; }
    }
}
