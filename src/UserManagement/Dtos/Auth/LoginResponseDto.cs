using UserManagement.Dtos.User;

namespace UserManagement.Dtos.Auth;

public class LoginResponseDto
{
    public string Message { get; set; } = string.Empty;
    public UserResponseDto User { get; set; } = default!;
    public string Token { get; set; } = string.Empty;
}