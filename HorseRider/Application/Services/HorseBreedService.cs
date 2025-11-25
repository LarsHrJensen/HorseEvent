using HorseRiderContext.Application.DTO_s;
using HorseRiderContext.Application.Interfaces;
using HorseRiderContext.Domain.Entities;

namespace HorseRiderContext.Application.Services
{
    public class HorseBreedService : IHorseBreedService
    {
        private IHorseBreedRepository _repository;

        public HorseBreedService (IHorseBreedRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<HorseBreedDTO>> GetAllHorseBreedsAsync()
        {
            List<HorseBreed> horseBreeds = await _repository.GetAllAsync();

            //map entitiy to dto
            return horseBreeds.Select(h => new HorseBreedDTO
            {
                Id = h.Id,
                Name = h.Name
            }).ToList();
        }
    }
}
