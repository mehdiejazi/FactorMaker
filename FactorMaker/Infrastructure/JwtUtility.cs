using FactorMaker.Infrastructure.ApplicationSettings;
using FactorMaker.Services.ServicesIntefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FactorMaker.Infrastructure
{
    public static class JwtUtility
    {
       
        public static string GenerateJwtToken(User user, AuthSettings mainSettings)
        {
            byte[] key = Encoding.ASCII.GetBytes(mainSettings.SecretKey);

            var symmetricSecurityKey = new SymmetricSecurityKey(key);

            var securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;

            var signingCredentials =
                new SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                            new Claim(type:nameof(user.FullName), value: user.FullName),
                            new Claim(type: ClaimTypes.Name, value: user.UserName),
                            new Claim(type: ClaimTypes.NameIdentifier , value: user.Id.ToString()),


                    }
                    ),
                //Issuer = "",
                //Audience = "",

                Expires = DateTime.UtcNow.AddMinutes(mainSettings.TokenExpiresInMinutes),

                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            string token = tokenHandler.WriteToken(securityToken);

            return token;

        }

        public static void AttachUserToContextByToken(HttpContext context, IUserService userService,
            string token, string secretKey)
        {
            try
            {
                byte[] key = Encoding.ASCII.GetBytes(secretKey);

                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token: token,
                    validationParameters: new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero

                    }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                
                if(jwtToken == null)
                {
                    return;
                }

                Claim userIdClaim =
                    jwtToken.Claims
                   .Where(c => c.Type.ToLower() == "NameId".ToLower())
                   .FirstOrDefault();

                var userId = Guid.Parse(userIdClaim.Value);

                User foundUser = userService.GetById(userId);

                if (foundUser == null) return;

                if (!foundUser.IsActive) return;
                //Check user is not disabled
                //Check user is nod deleted , ...

                context.Items["User"] = foundUser;
            }
            catch (Exception)
            {
                //do nothing if jwt authentication is failed
            }
        }
    }
}
