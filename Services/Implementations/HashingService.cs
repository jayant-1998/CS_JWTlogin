using EmployeeLoginPortal.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeLoginPortal.Services.Implementations
{
    public class HashingService : IHashingService
    {
        public string HashPassword(string plainPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public bool ComparePassword(string plainPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
                string hashedInputPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedInputPassword == hashedPassword;
            }
        }
    }
}
