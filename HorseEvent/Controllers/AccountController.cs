using HorseEvent.Data;
using HorseEvent.Views.Model;
using HorseEvent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HorseEvent.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
           
            if (model == null)
            {
                return BadRequest(new { message = "Invalid email or password." });
            }


            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });


                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);


                if (result.Succeeded)
                    return Ok(new { message = "Login successful." });

                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return StatusCode(500, new { message = "An error occurred while processing your request.", error = ex.Message });
            }


        }
    }
};

      
     
