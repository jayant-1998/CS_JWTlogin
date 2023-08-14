using JWTEmployeeLoginPortal.DAL.Entities;
using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Extensions;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using JWTLogin.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTLogin.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        //private readonly ICompressionProvider _config;

        public EmployeeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IEmployeeRepository>();
            //_config = configuration;
        }



        public async Task<RegistrationResponseViewModel?> RegistrationAsync(RegistrationRequestViewModel reg)
        {
            reg.Password = HashPassword(reg.Password);
            var temp =reg.ToViewModel<RegistrationRequestViewModel, Registration>();
            var entity = await _repository.RegistrationAsync(temp);
            if (entity != null)
            {
                return entity.ToViewModel<Registration, RegistrationResponseViewModel>();
            }
            return null;
        }
        static string HashPassword(string plainPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<string> LoginAsync(LoginRequestViewModel reg )
        {
            Secret? secretKey = await _repository.GetSecretKey();
            Registration? details = await _repository.LoginAsync(reg);
            if (details != null)
            {
                if (secretKey != null)
                {
                    if (ComparePassword(reg.Password, details.Password))
                    {
                        return GenerateJwt(details.UserId, secretKey.SecretKey);
                    }
                }
                return "Secret key is not found";
            }
            return "user not found";
        }
        private static string GenerateJwt(int userId,string secrete)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrete));
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
        private static bool ComparePassword(string plainPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                string hashedInputPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedInputPassword == hashedPassword;
            }
        }
    }
}
