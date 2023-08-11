using JWTLogin.DAL.DBContexts;
using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Extensions;
using JWTLogin.Models.RequestViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTLogin.DAL.Repositories.Implementations
{
    public class JWTEmployeeRepository : IJWTEmployeeRepository
    {
        

        private readonly JWTEmployeeDbContext _context;
        
        public JWTEmployeeRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<JWTEmployeeDbContext>();
        }
        public async Task<RegisteredEntity> RegistrationAsync(RegisteredEntity registration)
        {
            await _context.AddAsync(registration);
            await _context.SaveChangesAsync();
            return registration;
        }
        public async Task<string> LoginAsync(LoginRequestViewModel reg)
        {
            var response = await _context.registrations
                                .Where(r => r.Email == reg.Email)
                                .FirstOrDefaultAsync();

            if (response != null)
            {
                if (ComparePassword(reg.Password, response.Password))
                    {
                    return GenerateJwt(response.UserId, "JWTTokenSecretKey");
                    }
                return "password does not match";
            }
            return "email does not match";
        }

        static bool ComparePassword(string plainPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                string hashedInputPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedInputPassword == hashedPassword;
            }
        }
        static string GenerateJwt(int userId, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
