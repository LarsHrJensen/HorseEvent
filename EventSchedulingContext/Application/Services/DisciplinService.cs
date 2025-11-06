using EventSchedulingContext.Application.DTOs;
using EventSchedulingContext.Application.Interfaces;

namespace EventSchedulingContext.Application.Services
{
    public class DisciplinService : IDisciplineService
    {
        private readonly IDisciplineRepository _disciplinRepository;

        public DisciplinService(IDisciplineRepository disciplineRepository)
        {
            _disciplinRepository = disciplineRepository;
        }
        public async Task<IEnumerable<DisciplinDTO>> GetAllDisciplinesAsync()
        {
            var disciplins = await _disciplinRepository.GetAllAsync();

            return disciplins.Select(d => new DisciplinDTO
            {
                Id = d.Id,
                Name = d.Name
            });
        }
    }
}
