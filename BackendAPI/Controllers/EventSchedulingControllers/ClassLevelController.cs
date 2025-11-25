using EventSchedulingContext.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.EventSchedulingContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassLevelController : ControllerBase
    {
        private readonly IClassLevelService _classLevelService;

        public ClassLevelController(IClassLevelService classLevelService)
        {
            _classLevelService = classLevelService;
        }
        // GET: api/classlevel
        [HttpGet]
        public async Task<IActionResult> GetAllClassLevels()
        {
            var disciplines = await _classLevelService.GetAllClassLevelsAsync();

            return Ok(disciplines);
        }
    }
}
