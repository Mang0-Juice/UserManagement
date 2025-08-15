using UserManagement.Dtos.User;

namespace UserManagement.Utils;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserResponseDto dto);
}