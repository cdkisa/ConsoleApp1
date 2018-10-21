using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConsoleApp1.Common;

namespace ConsoleApp1.Tokens
{
    public class SimpleTokenValidator : ISimpleTokenValidator
    {
        private static ClaimsPrincipal GetPrincipal(string secret, string token, int timeToLiveInMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            if (jwtToken == null)
                return null;

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var parameters = new TokenValidationParameters()
            {
                IssuerSigningKey = signingKey, 
                ClockSkew = TimeSpan.FromMinutes(timeToLiveInMinutes)
            };

            var tokenValidationResult = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            
            return tokenValidationResult;
        }

        public IValidationResult Validate(string secret, string token, int timeToLiveInMinutes = 5)
        {
            try
            {
                var principal = GetPrincipal(secret, token, timeToLiveInMinutes);
                var identity = (ClaimsIdentity)principal.Identity;
                var nameClaim = identity.FindFirst(ClaimTypes.Name);

                return ValidationResult.Success(nameClaim.Value);
            }
            catch(SecurityTokenExpiredException stee)
            {
                return ValidationResult.Expired(stee.Message);
            }
            catch (Exception ex)
            {
                return ValidationResult.Fail(ex.Message);
                
            }
        }
    }
 }
