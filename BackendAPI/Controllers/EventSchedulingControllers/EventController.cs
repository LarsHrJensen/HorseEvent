using ClubContext.Application.Interfaces;
using ClubContext.Application.Services;
using Contracts.Club;
using Contracts.Events;
using Contracts.Horses;
using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Application.Mapper;
using HorseRiderContext.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.EventSchedulingContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Simpel controller-level validering (tommelfingerregel: kun "request sanity checks")
            if (request.StartDate > request.EndDate)
                return BadRequest("StartDate must be before EndDate.");

            if (request.Classes is null || request.Classes.Count == 0)
                return BadRequest("At least one class must be provided.");

            //mapping fra request til DTO
            // --- Her kalder vi mapperen ---
            var dto = EventMapper.MapToDTO(request);

            // Send DTO videre til service
            var createdEvent = await _eventService.CreateEventAsync(dto);

            // Returner resultat
            return Ok(createdEvent);
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var eventDTO = await _eventService.GetAllEventsAsync();

            //Mapping

            return Ok(eventDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventResponse>> GetById(int id)
        {
            var evt = await _eventService.GetByIdAsync(id); // mapper EF entity til EventResponse

            if (evt == null)
                return NotFound($"Stævne med id {id} blev ikke fundet.");

            var response = EventMapper.MapToResponse(evt);

            return Ok(evt); // sender EventResponse som JSON
        }
    }
}
