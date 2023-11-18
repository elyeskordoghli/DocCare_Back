using System.ComponentModel.DataAnnotations;

namespace DocCare_Backend.Models
{
    public class UserLogin
    {
        [Key]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
