using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementContext.Application.DTOs;
using UserManagementContext.Application.Interfaces;


namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Brugerdata er tomt." });

            // map til DTO
            var registerDto = new RegisterUserDto
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.RegisterAsync(registerDto);

            if (result == null)
                return BadRequest(new { message = "Kunne ikke oprette bruger." });

            return Ok(result);
        }
    }
}
