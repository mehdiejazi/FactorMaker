using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class LoginRequestViewModel
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
