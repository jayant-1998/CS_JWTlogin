using System.ComponentModel.DataAnnotations;

namespace JWTLogin.Models.RequestViewModels
{
    public class RegistrationRequestViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }
}
