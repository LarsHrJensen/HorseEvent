using Contracts;
using Contracts.Horses;
using HorseRider.Application.Commands;
using HorseRider.Application.Handlers;
using HorseRider.Application.Queries;
using HorseRiderContext.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.HorseRiderContext
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorseController : ControllerBase
    {

        //private readonly CreateHorseHandler _createHorseHandler;
        private readonly IMediator _mediator;

        public HorseController(CreateHorseHandler createBookHandler, IMediator mediator)
        {
            //_createHorseHandler = createBookHandler;
            _mediator = mediator;
        }
        // POST: api/heste
        [HttpPost]
        public async Task<IActionResult> CreateHorseAsync([FromBody] CreateHorseRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Hest data er tomt." });

            var command = new CreateHorseCommand(request.Name, request.HorseId, request.Height, request.BirthYear, request.Gender, request.Color, request.Breed, request.Breeder, request.SireId, request.DamId);
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
                Category = h.Category,
                Gender = h.Gender,
                Breeder = h.Breeder,
                BreedId = h.BreedId,
                BreedName = h.BreedName,
                DamId = h.DamId,
                SireId = h.SireId,
                Color = h.Color

            });

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHorses(
                                                        [FromQuery] string? name,
                                                        [FromQuery] string? ueln,
                                                        [FromQuery] int? birthYear,
                                                        [FromQuery] int? raceId)
        {
            var horsesDTO = await _mediator.Send(new SearchHorsesQuery(name, ueln, birthYear, raceId));

            var response = horsesDTO.Select(h => new HorseResponse
            {
                Id = (int)h.Id,
                Name = h.HorseName,
                UELN = h.UELN,
                Height = h.HorseHeight,
                BirthYear = h.BirthYear,
                Category = h.Category,
                Gender = h.Gender,
                Breeder = h.Breeder,
                BreedName = h.BreedName,
                BreedId = h.BreedId,
                DamId = h.DamId,
                SireId = h.SireId,
                Color = h.Color
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HorseResponse>> GetById(int id)
        {

            var horseDTO = await _mediator.Send(new GetHorseByIdQuery(id));

            if (horseDTO == null)
                return NotFound($"Hest med id {id} blev ikke fundet.");

            return Ok(horseDTO);
        }
    }
}
