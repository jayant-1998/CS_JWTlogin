using JWTEmployeeLoginPortal.DAL.Entities;
using JWTLogin.DAL.Entities;
using JWTLogin.Models.RequestViewModels;

namespace JWTLogin.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<Registration> RegistrationAsync(Registration reg);

        public Task<Registration?> LoginAsync(LoginRequestViewModel reg);
        public Task<Secret?> GetSecretKey();
    }
}
