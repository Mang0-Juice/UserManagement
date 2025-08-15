using Microsoft.AspNetCore.Mvc;
using UserManagement.Dtos.Auth;
using UserManagement.Dtos.User;
using UserManagement.Services;
using UserManagement.Utils;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthController(IUserService userService, IJwtTokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userDto = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);

            if (userDto == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var userEntity = await _userService.GetEntityByIdAsync(userDto.Id); 
            if (userEntity == null)
                return Unauthorized(new { message = "User entity not found" });

            var token = _tokenGenerator.GenerateToken(userEntity);

            var response = new LoginResponseDto
            {
                Message = "Login successful",
                User = userDto,
                Token = token
            };

            return Ok(response);
        }
    }
}
