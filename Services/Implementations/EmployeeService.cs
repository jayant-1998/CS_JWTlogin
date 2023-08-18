using JWTEmployeeLoginPortal.DAL.Entities;
using JWTLogin.DAL.Entities;
using JWTLogin.DAL.Repositories.Interfaces;
using JWTLogin.Extensions;
using JWTLogin.Models.RequestViewModels;
using JWTLogin.Models.ResponseViewModels;
using JWTLogin.Services.Interfaces;
using EmployeeLoginPortal.Services.Implementations;
using System.Text.RegularExpressions;
using EmployeeLoginPortal.Models.ResponseViewModels;

namespace JWTLogin.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {

        private const string Pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IServiceProvider serviceProvider)
        {
            _repository = serviceProvider.GetRequiredService<IEmployeeRepository>();

        }

        public async Task<RegistrationResponseViewModel?> RegistrationAsync(RegistrationRequestViewModel reg)
        {
            var pass = new HashingService();
            reg.Password = pass.HashPassword(reg.Password);
            var temp = reg.ToViewModel<RegistrationRequestViewModel, Registration>();
            if (!Regex.Match(reg.Email, Pattern).Success)
            {
                return null;
            }
            var entity = await _repository.RegistrationAsync(temp);
            if (entity != null)
            {
                return entity.ToViewModel<Registration, RegistrationResponseViewModel>();
            }
            return null;
        }

        public async Task<string> LoginAsync(LoginRequestViewModel reg)
        {
            var pass = new HashingService();
            var jwt = new TokenGenerateService();
            var encrypt = new EncryptDecryptStringService();

            TokenKey? key = await _repository.GetSecretKey();
            if (key == null)
            {
                return "something went wrong!!!!";
            }
            string SecretsKey = encrypt.DecryptString(key.EncryptionKey, key.SecretKeyEncrypted);
            Registration? details = await _repository.LoginAsync(reg);
            if (details != null)
            {
                if (SecretsKey != null)
                {
                    if (pass.ComparePassword(reg.Password, details.Password))
                    {
                        return jwt.GenerateJwt(details, SecretsKey);
                    }
                }
                return "Secret key is not found";
            }
            return "user not found";
        }

        public string Encryption(string k, string t)
        {
            var encrypt = new EncryptDecryptStringService();
            string key = k;
            string token = t;
            return encrypt.EncryptString(key, token);
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(string? jwtToken, int id)
        {
            var encrypt = new EncryptDecryptStringService();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                TokenKey? key = await _repository.GetSecretKey();
                if (key == null)
                {
                    throw new Exception("Error in GetSecretKey not Fetching data");
                }
                string SecretsKey = encrypt.DecryptString(key.EncryptionKey, key.SecretKeyEncrypted);
                var claimObject = new FetchClaimsService();
                ClaimsViewModel claim = claimObject.IsTokenValid(jwtToken, SecretsKey);
                if (claim.IsValid == false)
                {
                    throw new Exception($"{jwtToken} is not valid.");
                }
                if (claim.ExpiredAt > DateTime.Now && claim.Role == "user" || claim.Role == "admin")
                {
                    id = claim.UserId;
                }
                Registration reg = await _repository.FetchDetailsAsyncById(id);
                if (reg != null)
                {
                    return reg.ToViewModel<Registration, EmployeeViewModel>();
                }
                throw new Exception("Error in FetchDetailsAsyncById not Fetching data");
            }
            else if (id >= 0)
            {
                Registration reg = await _repository.FetchDetailsAsyncById(id);
                if (reg != null)
                {
                    return reg.ToViewModel<Registration, EmployeeViewModel>();
                }
                throw new Exception("Error in FetchDetailsAsyncById not Fetching data");
            }
            throw new Exception("Either JwtToken or Id is required");
        }
    }
}
