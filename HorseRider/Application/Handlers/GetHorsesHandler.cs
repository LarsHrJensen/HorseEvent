using global::HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Application.Queries;
using MediatR;

namespace HorseRider.Application.Handlers
{
    public class GetHorsesHandler : IRequestHandler<GetHorsesQuery, List<HorseDTO>>
    {
        private readonly IHorseRepository _horseRepository;

        public GetHorsesHandler(IHorseRepository _horseRepository)
        {
            this._horseRepository = _horseRepository;
        }

        public async Task<List<HorseDTO>> Handle(GetHorsesQuery request, CancellationToken cancellationToken)
        {
            var books = await _horseRepository.GetAllAsync();

            return books.Select(h => new HorseDTO
            {
                HorseName = h.Name,
                Id = h.Id,
                HorseHeight = h.Height,
                BirthYear = h.BirthYear

            }).ToList();
        }
    }
    

}
