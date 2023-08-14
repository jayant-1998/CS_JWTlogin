namespace JWTEmployeeLoginPortal.Models.ResponseViewModels
{
    public class SecretResponseViewModel
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
