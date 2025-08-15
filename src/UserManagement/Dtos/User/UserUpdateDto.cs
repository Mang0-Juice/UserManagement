using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dtos.User
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 30 and 50 characters")]
        public string Username { get; set; } = string.Empty;
    }
}
