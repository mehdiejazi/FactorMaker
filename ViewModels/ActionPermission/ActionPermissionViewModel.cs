using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.RoleActionPermission;

namespace ViewModels.ActionPermission
{
    public class ActionPermissionViewModel : ViewModelBase
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Url { get; set; }
        //public virtual ICollection<RoleActionPermissionViewModel> RoleActionPermission { get; set; }
    }
}
