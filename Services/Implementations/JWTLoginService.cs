using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Extensions;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using JWTLogin.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace JWTLogin.Services.Implementations
{
    public class JWTLoginService : IJWTLoginService
    {
        private readonly IJWTLoginRepository _repository;

        public JWTLoginService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IJWTLoginRepository>();
        }
        public async Task<RegistrationResponseViewModel?> InsertDataAsync(RegistrationRequestViewModel reg)
        {
            reg.password = HashPassword(reg.password);
            var entity = await _repository.InsertDataAsync(reg);
            if (entity != null)
            {
                return entity.ToViewModel<RegisteredEntity, RegistrationResponseViewModel>();
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

        public async Task<string> LoginAsync(LoginRequestViewModel reg)
        {
            return await _repository.LoginAsync(reg);
        }
    }
}
