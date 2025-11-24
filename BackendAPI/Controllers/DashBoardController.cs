using HorseRiderContext.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorseRiderContext.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AdvQueriesForDashRepository _repo;

        public DashboardController(AdvQueriesForDashRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("TopRiders/{year}")]
        public async Task<IActionResult> GetTopRiders(int year, int top = 10)
        {
            var data = await _repo.GetTopRidersAsync(year, top);
            return Ok(data);
        }
    }
}