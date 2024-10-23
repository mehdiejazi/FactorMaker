using System;

namespace ViewModels.User
{
    public class ChangePasswordViewModel
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
