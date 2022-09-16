using Models.Enums;
using System;
using ViewModels.Base;

namespace ViewModels
{
    public class UserViewModel:PersonViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
