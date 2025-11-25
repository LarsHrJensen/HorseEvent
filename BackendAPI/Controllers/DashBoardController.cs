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

        [HttpGet("riderconsistency")]
        public async Task<IActionResult> GetRiderConsistency()
        {
            try
            {
                var data = await _repo.GetRiderConsistencyAsync(); 
                if (data == null || !data.Any())
                    return NotFound("Ingen data fundet");

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log evt. fejl
                return StatusCode(500, $"Intern serverfejl: {ex.Message}");
            }
        }

        [HttpGet("RiderHorsePerformance")]
        public async Task<IActionResult> GetRiderHorsePerformance()
        {
            try
            {
                var data = await _repo.GetRiderHorsePerformanceAsync();
                if (data == null || !data.Any())
                    return NotFound("Ingen data fundet");

                return Ok(data);
            }
            catch (Exception ex)
            {
                // Log fejl hvis ønsket
                return StatusCode(500, $"Intern serverfejl: {ex.Message}");
            }
        }
    }
}