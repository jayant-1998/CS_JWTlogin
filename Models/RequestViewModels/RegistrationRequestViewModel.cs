using System.ComponentModel.DataAnnotations;

namespace JWTLogin.Models.RequestViewModels
{
    public class RegistrationRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
