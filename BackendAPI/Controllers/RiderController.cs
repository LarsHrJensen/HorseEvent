using Contracts;
using HorseRider.Application.Commands;
using HorseRider.Application.Handlers.HorseRider.Application.Handlers;
using HorseRider.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NewBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiderController : ControllerBase
    {
        private readonly CreateRiderHandler _createRytterHandler;
        private readonly IMediator _mediator;

        public RiderController(CreateRiderHandler createRiderHandler, IMediator mediator)
        {
            _createRytterHandler = createRiderHandler;
            _mediator = mediator;
        }

        // POST: api/riders
        [HttpPost]
        public async Task<IActionResult> CreateRytterAsync([FromBody] CreateRiderRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Rytter data er tomt." });

            var command = new CreateRiderCommand(request.Name, request.Email, request.BirthYear);
            var rytterDTO = await _mediator.Send(command);

            if (rytterDTO == null)
                return StatusCode(500, new { message = "Kunne ikke oprette rytteren." });

            var response = new RiderResponse
            {
                Id = (int)rytterDTO.Id,
                Name = rytterDTO.RiderName,
                BirthYear = rytterDTO.BirthYear,
                Email = rytterDTO.Email
            };

            return Ok(response);
        }

        // GET: api/riders
        [HttpGet]
        public async Task<IActionResult> GetAllRyttere()
        {
            var ryttereDTO = await _mediator.Send(new GetRidersQuery());

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