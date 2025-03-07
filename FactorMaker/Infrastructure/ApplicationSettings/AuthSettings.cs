﻿namespace FactorMaker.Infrastructure.ApplicationSettings
{
    public class AuthSettings
    {
        public int TokenExpiresInMinutes { get; set; }
        public int RefreshTokenExpiresInMinutes { get; set; }
        public string SecretKey { get; set; }
    }
}
