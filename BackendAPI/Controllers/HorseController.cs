using Contracts;
using HorseRider.Application.Commands;
using HorseRider.Application.Handlers;
using HorseRider.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ReactApp1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorseController : ControllerBase
    {

        private readonly CreateHorseHandler _createHorseHandler;
        private readonly IMediator _mediator;

        public HorseController(CreateHorseHandler createBookHandler, IMediator mediator)
        {
            _createHorseHandler = createBookHandler;
            _mediator = mediator;
        }
        // POST: api/heste
        [HttpPost]
        public async Task<IActionResult> CreateHorseAsync([FromBody] CreateHorseRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Hest data er tomt." });

            var command = new CreateHorseCommand(request.Name, request.HorseId, request.Height, request.BirthYear);
            var horseDTO = await _mediator.Send(command);

            if (horseDTO == null)
                return StatusCode(500, new { message = "Kunne ikke oprette hesten." });

            var response = new HorseResponse
            {
                Id = (int)horseDTO.Id,
                UELN = horseDTO.UELN,
                Name = horseDTO.HorseName,
                Height = horseDTO.HorseHeight,
                BirthYear = horseDTO.BirthYear
            };

            return Ok(response); // Returnér altid JSON
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHorses()
        {
            var horsesDTO = await _mediator.Send(new GetHorsesQuery());

            var response = horsesDTO.Select(h => new HorseResponse
            {
                Id = (int)h.Id,
                UELN = h.UELN,
                Name = h.HorseName,
                Height = h.HorseHeight,
                BirthYear = h.BirthYear,
                Category = h.Category
            });

            return Ok(response);
        }
    }
}