using JWTLogin.DAL.Entities;

namespace EmployeeLoginPortal.Services.Interfaces
{
    public interface ITokenGenerateService
    {
        public string GenerateJwt(Registration reg, string secrete);
    }
}
