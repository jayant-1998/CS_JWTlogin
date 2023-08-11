using JWTLogin.DAL.Entities;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;

namespace JWTLogin.Services.Interfaces
{
    public interface IJWTEmployeeService
    {
        public Task<RegistrationResponseViewModel> RegistrationAsync(RegistrationRequestViewModel reg);
        public Task<string> LoginAsync(LoginRequestViewModel reg);
    }
}
