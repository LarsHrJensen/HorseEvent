using ClubContext.Application.DTOs;
using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;
using ClubContext.Domain.ValueObjects;
using ClubContext.Application.Mappers;
using SharedKernel;
using System.Text.Json;

namespace ClubContext.Application.Services
{
    public class ClubService:IClubService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClubService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                  apartment: address.Apartment
                )
            };

            await _unitOfWork.Clubs.AddAsync(club);

            // Tilføj Outbox event
            var outboxEvent = new OutboxEvent
            {
                EventType = "ClubCreated",
                Payload = JsonSerializer.Serialize(club.ToDto())
            };
            await _unitOfWork.Outbox.AddAsync(outboxEvent);

            // Commit begge ændringer i samme transaction
            await _unitOfWork.CompleteAsync();

            return club.ToDto();
        }


        public async Task<IEnumerable<ClubDto>> GetAllClubsAsync()
        {
            List<Club> clubs = await _unitOfWork.Clubs.GetAllAsync();

            return clubs.Select(c => c.ToDto());
        }

        public Task<ClubDto?> GetClubAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

