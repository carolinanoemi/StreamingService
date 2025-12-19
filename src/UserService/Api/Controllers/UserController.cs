using Microsoft.AspNetCore.Mvc;
using UserService.Api.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();

            var response = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            });

            return Ok(response);
        }

        // GET: api/user/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return Ok(response);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto)
        {
            var user = await _userService.CreateUserAsync(dto.UserName, dto.Email, dto.Password);

            var response = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
        }

        // PUT: api/user/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _userService.UpdateUserAsync(id, dto.Email, dto.Password);
            if (user == null)
                return NotFound();

            var response = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return Ok(response);
        }

        // DELETE: api/user/5 (soft delete)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var success = await _userService.DeactivateUserAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
