using EmployeeLoginPortal.Models.ResponseViewModels;

namespace EmployeeLoginPortal.Services.Interfaces
{
    public interface IFetchClaimsService
    {
        public ClaimsViewModel FetchClaims(string jwtToken);
        public ClaimsViewModel IsTokenValid(string jwtToken, string publicKey);

    }
}
