using Microsoft.AspNetCore.Mvc;
using UserManagement.Dtos.User;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _service.RegisterUserAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
        {
            dto.Id = id;
            var updated = await _service.UpdateUserAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _service.RemoveUserAsync(id);
            if (removed == null) return NotFound();
            return Ok(removed);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetEntityByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            });
        }
    }
}
