using Models.Enums;
using ViewModels.Base;

namespace ViewModels
{
    public class UserViewModel:PersonViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public RoleType Role { get; set; }
    }
}
