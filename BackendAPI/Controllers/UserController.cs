using Microsoft.AspNetCore.Mvc;
using UserManagementContext.Application.Interfaces;
using Contracts;
using Contracts.User;

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
    }
}
