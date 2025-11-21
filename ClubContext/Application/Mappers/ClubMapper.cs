using ClubContext.Application.DTOs;
using ClubContext.Domain.Entities;

namespace ClubContext.Application.Mappers
{
    public static class ClubMapper
    {
        public static ClubDto ToDto(this Club club)
        {
            return new ClubDto
            {
                ClubId = club.ClubId,
                Name = club.Name,
                Address = new AddressDto
                {
                    StreetName = club.Adress.StreetName,
                    StreetNumber = club.Adress.HouseNumber,
                    Apartment = club.Adress.Apartment,
                    PostalCode = club.Adress.PostalCode,   // direkte felt
                    City = club.Adress.City,               // direkte felt
                    CountryCode = club.Adress.CountryCode, // direkte felt
                    CountryName = club.Adress.CountryName  // direkte felt
                }
            };
        }
    }

}
