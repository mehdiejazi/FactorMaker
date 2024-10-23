using Models.Base;
using System;
using System.Collections.Generic;

namespace Models
{
    public class User : PersonBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailIsVerified { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ImageAsset Avatar { get; set; }
        public virtual UserRefreshToken RefreshToken { get; set; }
        public virtual ICollection<ImageAsset> ImageAssets { get; set; }
        public bool IsForcedChangePassword { get; set; }
    }
}
