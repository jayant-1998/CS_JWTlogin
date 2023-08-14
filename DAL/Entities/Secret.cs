using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JWTEmployeeLoginPortal.DAL.Entities
{
    [Table("secretkey", Schema = "dbo")]
    public class Secret
    {
        [Key]
        public int Id { get; set; }
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
