using Models.Base;
using System;

namespace Models
{
    public class UserRefreshToken : EntityBase
    {
        public Guid OwnerId { get; set; }
        public virtual User User { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenTimeOut { get; set; }
        public bool IsValid { get; set; }
    }
}
