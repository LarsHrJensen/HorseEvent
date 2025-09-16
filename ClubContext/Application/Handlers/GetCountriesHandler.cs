using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Queries;
using MediatR;

namespace ClubContext.Application.Handlers
{
    public class GetCountriesHandler : IRequestHandler<GetCountriesQuery, List<CountryDTO>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountriesHandler(ICountryRepository _countryRepository)
        {
            this._countryRepository = _countryRepository;
        }

        public async Task<List<CountryDTO>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetAllAsync();

            return countries.Select(c => new CountryDTO
            {
                Name = c.Name,
                Code = c.Code,

            }).ToList();
        }
    }
}
