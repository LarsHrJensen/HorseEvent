using HorseRider.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using HorseRider.Infrastructure.Repositories;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ResultController : ControllerBase
    {
        private readonly ResultRepository _resultRepository;
        private readonly ClassRepository _classRepository;

        public ResultController(ResultRepository resultRepository, ClassRepository classRepository)
        {
            _resultRepository = resultRepository;
            _classRepository = classRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _resultRepository.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _resultRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Result newResult)
        {
            if (newResult == null)
                return BadRequest("Result object was null.");

            await _resultRepository.AddAsync(newResult);

            return CreatedAtAction(nameof(GetById), new { id = newResult.ResultId }, newResult);
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClass(int classid)
        {
            var classEntity = await _classRepository.GetByIdAsync(classid);
            if (classEntity == null)
                return NotFound($"Class with ID {classid} not found.");

            var results = await _resultRepository.GetByClassIdAsync(classid);

            if (classEntity.Discipline == "Dressage")
            {
                results = results.OrderBy(r => r.Score).ToList();
            }

            return Ok(results);
        }

    }
}
