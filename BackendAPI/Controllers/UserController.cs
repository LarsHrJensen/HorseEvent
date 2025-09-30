using Microsoft.AspNetCore.Mvc;
using UserManagementContext.Application.Interfaces;
using Contracts;
using Contracts.User;
using Microsoft.AspNetCore.Identity.Data;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService clubService)
        {
            _userService = clubService;
        }
        // POST: api/brugere
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {

            if (request == null)
                return BadRequest(new { message = "´bruger data er tomt." });

            var userDTO = await _userService.CreateUserAsync(request);

            if (userDTO == null)
                return StatusCode(500, new { message = "Kunne ikke oprette brugeren." });

            var response = new UserResponse
            {
                Id = userDTO.Id,
                UserName = userDTO.Username,
                Email = userDTO.Email
            };

            return Ok(response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var user = await _userService.LoginAsync(request);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var response = new UserResponse
            {
                Id = user.Id,
                UserName = user.Username,
                Email = user.Email
            };

            return Ok(response); // evt. sammen med JWT-token
        }
    }
}
