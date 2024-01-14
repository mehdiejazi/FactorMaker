using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class LoginRequestViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
