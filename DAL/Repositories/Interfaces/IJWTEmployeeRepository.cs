using JWTLogin.DAL.Entities;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;

namespace JWTLogin.DAL.Repositories.Interfaces
{
    public interface IJWTEmployeeRepository
    {
        public Task<RegisteredEntity> RegistrationAsync(RegisteredEntity reg);

        public Task<string> LoginAsync(LoginRequestViewModel reg);
    }
}
