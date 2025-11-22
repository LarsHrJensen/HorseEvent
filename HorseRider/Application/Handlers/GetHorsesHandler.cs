using global::HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Application.Queries;
using MediatR;

namespace HorseRider.Application.Handlers
{
    public class GetUsersHorsesHandler : IRequestHandler<GetHorsesQuery, List<HorseDTO>>
    {
        private readonly IHorseRepository _horseRepository;

        public GetUsersHorsesHandler(IHorseRepository _horseRepository)
        {
            this._horseRepository = _horseRepository;
        }

        public async Task<List<HorseDTO>> Handle(GetHorsesQuery request, CancellationToken cancellationToken)
        {
            var horses = await _horseRepository.GetAllAsync();

            return horses.Select(h => new HorseDTO
            {
                Id = h.HorseId,
                HorseName = h.Name,
                UELN = h.UELN,
                HorseHeight = h.Height,
                BirthYear = h.BirthYear,
                Category = h.Category,
                Gender = h.Gender,
                Breeder = h.Breeder,
                BreedId = h.BreedId,
                BreedName = h.Breed?.Name,
                DamId = h.DamId ,
                SireId = h.SireId,
                Color = h.Color
            }).ToList();
        }
    }
}
