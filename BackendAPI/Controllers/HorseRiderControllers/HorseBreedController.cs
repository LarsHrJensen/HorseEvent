using HorseRiderContext.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers.HorseRiderContext
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorseBreedController : ControllerBase
    {
        private readonly IHorseBreedService _horseBreedService;

        public HorseBreedController(IHorseBreedService horseBreedService)
        {
            _horseBreedService = horseBreedService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllHorseBreeds()
        {
            var HorseBreedDTO = await _horseBreedService.GetAllHorseBreedsAsync();

            //Mapping

            return Ok(HorseBreedDTO);
        }
    }
}
