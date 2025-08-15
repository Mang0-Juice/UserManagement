using UserManagement.Dtos.User;

namespace UserManagement.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetEntityByIdAsync(Guid id);
        Task<UserResponseDto> RegisterUserAsync(UserRegisterDto registerDto);
        Task<UserResponseDto> UpdateUserAsync(UserUpdateDto updateDto);
        Task<UserResponseDto?> AuthenticateAsync(string email, string password);
        Task<UserResponseDto> RemoveUserAsync(Guid id);
    }
}
