using System.Security.Cryptography;
using AutoMapper;
using UserManagement.Dtos.User;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserResponseDto?> GetEntityByIdAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> RegisterUserAsync(UserRegisterDto registerDto)
        {
            ArgumentNullException.ThrowIfNull(registerDto);

            var email = registerDto.Email.Trim().ToLowerInvariant();
            var existing = await _repo.GetByEmailAsync(email);
            if (existing != null) throw new InvalidOperationException("A user with this email already exists.");

            CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = registerDto.Username.Trim(),
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var created = await _repo.CreateAsync(user);
            return _mapper.Map<UserResponseDto>(created);
        }

        public async Task<UserResponseDto> UpdateUserAsync(UserUpdateDto updateDto)
        {
            if (updateDto == null) throw new ArgumentNullException(nameof(updateDto));

            var user = await _repo.GetByIdAsync(updateDto.Id);
            if (user == null) throw new InvalidOperationException("User not found.");

            if (!string.IsNullOrWhiteSpace(updateDto.Username))
                user.Username = updateDto.Username.Trim();

            if (!string.IsNullOrWhiteSpace(updateDto.Email))
                user.Email = updateDto.Email.Trim().ToLowerInvariant();

            await _repo.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> RemoveUserAsync(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) throw new InvalidOperationException("User not found.");

            var success = await _repo.DeleteAsync(id);
            if (!success) throw new InvalidOperationException("Failed to remove user.");

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var normalizedEmail = email.Trim().ToLowerInvariant();
            var user = await _repo.GetByEmailAsync(normalizedEmail);

            if (user == null) return null;

            if (user.PasswordHash is null || user.PasswordSalt is null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return _mapper.Map<UserResponseDto>(user);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
