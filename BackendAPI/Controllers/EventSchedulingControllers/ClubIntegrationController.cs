using Contracts.Integrations;
using EventSchedulingContext.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.EventSchedulingControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubIntegrationController : ControllerBase
    {
        private readonly IClubCreatedEventHandler _clubcreatedEventHandler;

        public ClubIntegrationController(IClubCreatedEventHandler clubcreatedEventHandler)
        {
            _clubcreatedEventHandler = clubcreatedEventHandler;
        }

        [HttpPost("created")]
        public async Task<IActionResult> ClubCreated(ClubCreatedEvent dto)
        {
            await _clubcreatedEventHandler.Handle(dto);

            return Ok();
        }
    }
}
