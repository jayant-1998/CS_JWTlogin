using EmployeeLoginPortal.Services.Interfaces;
using JWTLogin.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeLoginPortal.Services.Implementations
{
    public class TokenGenerateService : ITokenGenerateService
    {
        public string GenerateJwt(Registration reg, string secrete)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrete));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, reg.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.Typ, reg.Role.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
