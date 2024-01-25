using System;


namespace ViewModels.User
{
    public class NewTokenRequestViewModel
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
