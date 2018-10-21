using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConsoleApp1.Tokens
{
    public class SimpleTokenProvider : ISimpleTokenProvider
    {
        public string Create(string value, string salt, int timeToLiveInMinutes = 5)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(salt));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateJwtSecurityToken(
                subject: new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, value) }),
                expires: DateTime.UtcNow.AddMinutes(timeToLiveInMinutes),
                signingCredentials: signingCredentials
            );

            return handler.WriteToken(token);
        }

    }
}
