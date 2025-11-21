using ClubContext.Application.DTOs;
using ClubContext.Application.Queries;
using Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.Club
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/countries
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var CountryDTO = await _mediator.Send(new GetCountriesQuery());

            var response = CountryDTO.Select(c => new CountryResponse
            {
                Code = c.Code,
                Name = c.Name
            });

            return Ok(response);
        }

        [HttpGet("{countryCode}/postal-codes")]
        public async Task<IActionResult> GetPostalCodes(string countryCode)
        {
            countryCode = countryCode.ToLower();
            List<PostalCodeDTO> postalCodes = new List<PostalCodeDTO>();

            if (countryCode == "dk")
            {
                // Hent fra egen database
                postalCodes = await _mediator.Send(new GetDKPostalCodesQuery());
            }
            else
            {
                // Hent fra ekstern API (fx via HttpClient)
                postalCodes = await ExternalPostalService.GetPostalCodesAsync(countryCode);
            }

            return Ok(postalCodes);
        }

    }
}
