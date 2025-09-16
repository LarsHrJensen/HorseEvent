using ClubContext.Application.Queries;
using Contracts;
using HorseRider.Application.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController (IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/riders
        [HttpGet]
        public async Task<IActionResult> GetAllRyttere()
        {
            var CountryDTO = await _mediator.Send(new GetCountriesQuery());

            var response = ryttereDTO.Select(r => new RiderResponse
            {
                Id = (int)r.Id,
                Name = r.RiderName,
                BirthYear = r.BirthYear,
                Email = r.Email
            });

            return Ok(response);
        }
        
    }
}
