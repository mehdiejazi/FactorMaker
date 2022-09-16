using Models.Base;
using Models.Enums;
using System;

namespace Models
{
    public class User : PersonBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        //public RoleType Role { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
