using Models.Base;

namespace Models
{
    public class User : PersonBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
