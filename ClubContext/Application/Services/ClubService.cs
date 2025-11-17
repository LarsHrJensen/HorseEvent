using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;
using ClubContext.Domain.ValueObjects;
using ClubContext.Application.Mappers;

namespace ClubContext.Application.Services
{
    public class ClubService:IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
            public async Task<ClubDto> CreateClubAsync(string name, AddressDto address)
        {
            var club = new Club
            {
                Name = name,
                Adress = new Adress(
                    streetName: address.StreetName,
                    houseNumber: address.StreetNumber,
                    postalCode: address.PostalCode,
                    city: address.City,
                    countryCode: address.CountryCode,
                    countryName: address.CountryName,
                    apartment: address.Apartment  // valgfri, kan udelades hvis null
                    )
            };

            await _clubRepository.AddAsync(club);

            return club.ToDto();
        }


        public async Task<IEnumerable<ClubDto>> GetAllClubsAsync()
        {
            List<Club> clubs = await _clubRepository.GetAllAsync();

            return clubs.Select(c => c.ToDto());
        }

        public Task<ClubDto?> GetClubAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

