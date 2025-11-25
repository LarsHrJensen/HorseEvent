using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRiderContext.Application.Builders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.Handlers
{
    // Query
    public record SearchHorsesQuery(string? Name, string? UELN, int? BirthYear, int? RaceId) : IRequest<List<HorseDTO>>;

    // Handler
    public class SearchHorsesQueryHandler : IRequestHandler<SearchHorsesQuery, List<HorseDTO>>
    {

        private readonly IHorseRepository _horseRepository;

        public SearchHorsesQueryHandler(IHorseRepository _horseRepository)
        {
            this._horseRepository = _horseRepository;
        }

        public async Task<List<HorseDTO>> Handle(SearchHorsesQuery request, CancellationToken cancellationToken)
        {
            // Hent alle heste fra repository
            var horses = await _horseRepository.GetAllAsync();

            // Brug Builder pattern til at filtrere i memory
            var queryBuilder = new HorseQueryBuilder(horses.AsQueryable())
                .FilterByName(request.Name)
                .FilterByUELN(request.UELN)
                .FilterByBirthYear(request.BirthYear)
                .FilterByRaceId(request.RaceId);

            var filteredHorses = queryBuilder.Build().ToList(); // her er det nu List<Horse>

            return filteredHorses.Select(h => new HorseDTO(h)).ToList();
        }
    }
}
