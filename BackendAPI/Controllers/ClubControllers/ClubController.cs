using Azure.Core;
using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Queries;
using Contracts;
using Contracts.Club;
using Contracts.Events;
using EventSchedulingContext.Application.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.Club
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
               , request.District
            );

            return Ok(result);

        }

        // GET: api/klubber
        [HttpGet]
        public async Task<ActionResult<ClubResponse>> GetAllClubs()
        {
            var clubDTOs = await _clubService.GetAllClubsAsync();

            //Mapping

            // Mapping DTO -> Response
            var clubResponses = clubDTOs.Select(clubDto => new Contracts.Club.ClubResponse
            {
                ClubId = clubDto.ClubId,
                Name = clubDto.Name,
                DistrictId = clubDto.DistrictId,
                Address = new Contracts.Club.ClubResponse.AddressDto
                {
                    StreetName = clubDto.Address.StreetName,
                    StreetNumber = clubDto.Address.StreetNumber,
                    PostalCode = clubDto.Address.PostalCode,
                    CountryCode = clubDto.Address.CountryCode,
                    City = clubDto.Address.City
                }
            }).ToList();


            return Ok(clubResponses);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClubResponse>> GetById(int id)
        {
            // Hent DTO fra service
            var clubDto = await _clubService.GetClubByIdAsync(id);

            if (clubDto == null)
                return NotFound($"Klub med id {id} blev ikke fundet.");

            // Manuelt map DTO -> Contract
            var clubResponse = new Contracts.Club.ClubResponse
            {
                ClubId = clubDto.ClubId,
                Name = clubDto.Name,
                DistrictId = clubDto.DistrictId,
                Address = new Contracts.Club.ClubResponse.AddressDto
                {
                    StreetName = clubDto.Address.StreetName,
                    StreetNumber = clubDto.Address.StreetNumber,
                    PostalCode = clubDto.Address.PostalCode,
                    CountryCode = clubDto.Address.CountryCode,
                    City = clubDto.Address.City
                }
               
            };

            return Ok(clubResponse); // sender som JSON
        }
    }
}
