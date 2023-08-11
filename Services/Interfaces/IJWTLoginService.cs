using JWTLogin.DAL.Entities;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;

namespace JWTLogin.Services.Interfaces
{
    public interface IJWTLoginService
    {
        public Task<RegistrationResponseViewModel> InsertDataAsync(RegistrationRequestViewModel reg);
        public Task<string> LoginAsync(LoginRequestViewModel reg);
    }
}
