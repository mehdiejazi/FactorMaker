using System;
using System.Collections.Generic;
using ViewModels.Base;
using ViewModels.Role;
using ViewModels.Store;

namespace ViewModels.User
{
    public class UserViewModel : PersonViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public RoleViewModel Role { get; set; }
        public Guid RoleId { get; set; }
        public virtual ICollection<StoreViewModel> Stores { get; set; }
    }
}
