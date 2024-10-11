using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class LoginDto
    {

        [Required]
        public string Email { get; set; } = null!;
        [Required]

        public string Password { get; set; } = null!;
    }
}
