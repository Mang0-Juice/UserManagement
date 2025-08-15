using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dtos.User
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")] 
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Password and password confirmation do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
