using HorseRiderContext.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiderPerformanceController : ControllerBase
    {
        private readonly RiderPerformanceRepository _repository;

        public RiderPerformanceController(RiderPerformanceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{DRFLicenceNr}")]
        public async Task<IActionResult> GetRIderPerformance(string DRFLicenceNr)
        {
            var performanceList = await _repository.GetByRiderDrfNrAsync(DRFLicenceNr);

            if (performanceList == null || performanceList.Count == 0)
                return NotFound("Ingen data fundet for denne rytter");

            return Ok(performanceList);
        }
    }
}
