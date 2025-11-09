using Azure.Core;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Queries;
using Contracts;
using Contracts.Club;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        // POST: api/klubber
        [HttpPost]
        public async Task<IActionResult> CreateClubAsync([FromBody] CreateClubRequest request)
        {

            if (request == null)
                   return BadRequest(new { message = "club data er tomt." });

            var result = await _clubService.CreateClubAsync
            (
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

        // GET: api/klubber
        [HttpGet]
        public async Task<IActionResult> GetAllClubs()
        {
            var ClubDTO = await _clubService.GetAllClubsAsync();

            //Mapping

            return Ok(ClubDTO);
        }
    }
}
