using ClubContext.Application.Interfaces;
using ClubContext.Application.Queries;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.ClubControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        // GET: api/district
        [HttpGet]
        public async Task<IActionResult> GetAllDistricts()
        {
            var districtDTO = await _districtService.GetAllDistrictsAsync();

            //Mapping

            return Ok(districtDTO);
        }
    }
}
