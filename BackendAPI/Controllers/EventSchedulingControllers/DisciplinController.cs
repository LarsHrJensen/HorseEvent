using ClubContext.Application.Interfaces;
using EventSchedulingContext.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.EventSchedulingContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinController : ControllerBase
    {
        private readonly IDisciplineService _disciplinService;

        public DisciplinController(IDisciplineService disciplinService)
        {
            _disciplinService = disciplinService;
        }
        // GET: api/discipliner
        [HttpGet]
        public async Task<IActionResult> GetAllDisciplines()
        {
            var disciplines = await _disciplinService.GetAllDisciplinesAsync();

            return Ok(disciplines);
        }
    }
}
