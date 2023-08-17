namespace EmployeeLoginPortal.Models.ResponseViewModels
{
    public class ClaimsViewModel
    {
        public bool IsValid { get; set; } = false;
        public int UserId { get; set; }
        public string IssuedAt { get; set; }
        public string Role { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
