using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Authentication
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(User user,string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException(paramName: nameof(user));
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(paramName: nameof(token));
            }

            Id = user.Id;
            Token = token;
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FullName = user.FullName;

        }

        public Guid Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
