using Contracts;
using HorseRider.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ClubController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/klubber
        [HttpPost]
        public async Task<IActionResult> CreateClubAsync([FromBody] CreateHorseRequest request)
        {
            //if (request == null)
            //    return BadRequest(new { message = "Hest data er tomt." });

            //var command = new CreateHorseCommand(request.Name, request.HorseId, request.Height, request.BirthYear);
            //var horseDTO = await _mediator.Send(command);

            //if (horseDTO == null)
            //    return StatusCode(500, new { message = "Kunne ikke oprette hesten." });

            var response = new HorseResponse
            {
            };

            return Ok(response); // Returnér altid JSON
        }
    }
}
