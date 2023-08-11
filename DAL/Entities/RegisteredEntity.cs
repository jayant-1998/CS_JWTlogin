using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTLogin.DAL.Entities
{
    [Table("JWT", Schema = "dbo")]
    public class RegisteredEntity
    {
        [Key]
        public int user_id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string name { get; set; }
        public int age { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? modified_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public bool is_deleted { get; set; } = false;
        public bool is_login { get; set; } = false;

    }
}
