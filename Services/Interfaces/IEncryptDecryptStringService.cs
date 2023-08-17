namespace EmployeeLoginPortal.Services.Interfaces
{
    public interface IEncryptDecryptStringService
    {
        public string EncryptString(string key, string plainText);
        public string DecryptString(string key, string cipherText);
        
    }
}
