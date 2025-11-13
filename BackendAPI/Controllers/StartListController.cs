using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StartListController : ControllerBase
    {
        private readonly StartListRepository _startListRepository;

        public StartListController(StartListRepository startListRepository)
        {
            _startListRepository = startListRepository;
        }

        [HttpGet("{competitionName}")]
        public async Task<IActionResult> GetStartListByCompetition(string competitionName)
        {
            var startList = await _startListRepository.GetFullStartListByCompetitionAsync(competitionName);

            if (startList == null)
            {
                return NotFound($"No start list found for competition '{competitionName}'.");
            }
            return Ok(startList);
        }
    }
}
