using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string phone { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[!@#$%&amp;*()_+}{&quot;:;'?/gt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 uppercase,1 lowercase,1 number,1 Non alphanumeric and at least 6 Character")]
        public string Password { get; set; } = null!;
    }
}
