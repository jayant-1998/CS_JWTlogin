using JWTLogin.DAL.DBContexts;
using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Extensions;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTLogin.DAL.Repositories.Implementations
{
    public class JWTLoginRepository : IJWTLoginRepository
    {
        

        private readonly JWTLoginDbContext _context;
        
        public JWTLoginRepository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<JWTLoginDbContext>();
        }
        public async Task<RegisteredEntity> InsertDataAsync(RegistrationRequestViewModel registration)
        {
            var registrationEntity = registration.ToViewModel<RegistrationRequestViewModel, RegisteredEntity>();
            await _context.AddAsync(registrationEntity);
            await _context.SaveChangesAsync();
            return registrationEntity;
        }
        public async Task<string> LoginAsync(LoginRequestViewModel reg)
        {
            var response = await _context.registrations
                                .Where(r => r.email == reg.email)
                                .FirstOrDefaultAsync();

            if (response != null)
            {
                if (ComparePassword(reg.password, response.password))
                    {
                    return GenerateJwt(response.user_id, "JWTTokenSecretKey");
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
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
