using System;
using ViewModels.User;

namespace ViewModels.Authentication
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(UserViewModel user, string token)
        {
            User = user;
            Token = token;
        }
        public UserViewModel User { get; set; }
        public string Token { get; set; }
    }
}
