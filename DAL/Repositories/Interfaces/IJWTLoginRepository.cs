using JWTLogin.DAL.Entities;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;

namespace JWTLogin.DAL.Repositories.Interfaces
{
    public interface IJWTLoginRepository
    {
        public Task<RegisteredEntity> InsertDataAsync(RegistrationRequestViewModel reg);

        public Task<string> LoginAsync(LoginRequestViewModel reg);
    }
}
