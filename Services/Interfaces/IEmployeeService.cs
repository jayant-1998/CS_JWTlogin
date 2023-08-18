using EmployeeLoginPortal.Models.ResponseViewModels;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;

namespace JWTLogin.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<RegistrationResponseViewModel> RegistrationAsync(RegistrationRequestViewModel reg);
        public Task<string> LoginAsync(LoginRequestViewModel reg);
        public string Encryption(string k, string t);
        public Task<EmployeeViewModel> GetEmployeeAsync(string? jwt,int id);
    }
}
