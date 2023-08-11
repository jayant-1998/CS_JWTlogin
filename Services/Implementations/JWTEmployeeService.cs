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
    public class JWTEmployeeService : IJWTEmployeeService
    {
        private readonly IJWTEmployeeRepository _repository;

        public JWTEmployeeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IJWTEmployeeRepository>();
        }
        public async Task<RegistrationResponseViewModel?> RegistrationAsync(RegistrationRequestViewModel reg)
        {
            reg.Password = HashPassword(reg.Password);
            var temp =reg.ToViewModel<RegistrationRequestViewModel, RegisteredEntity>();
            var entity = await _repository.RegistrationAsync(temp);
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
