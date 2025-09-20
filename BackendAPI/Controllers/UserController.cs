using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _clubService;

        public UserController(IUserService clubService)
        {
            _clubService = clubService;
        }
        // POST: api/klubber
        [HttpPost]
        public async Task<IActionResult> CreateClubAsync([FromBody] CreateClubRequest request)
        {

            if (request == null)
                return BadRequest(new { message = "´bruger data er tomt." });

            var result = await _clubService.CreateClubAsync(
                request.Name,
          new ClubContext.Application.DTOs.AddressDto
          {
              StreetName = request.Address.StreetName,
              StreetNumber = request.Address.StreetNumber,
              City = request.Address.City,
              PostalCode = request.Address.PostalCode,
              CountryCode = request.Address.CountryCode,
              CountryName = request.Address.CountryName
          }
      );

            return Ok(result);

        }
    }
}
