namespace EmployeeLoginPortal.Services.Interfaces
{
    public interface IHashingService
    {
        public string HashPassword(string plainPassword);
        public bool ComparePassword(string plainPassword, string hashedPassword);
    }
}
