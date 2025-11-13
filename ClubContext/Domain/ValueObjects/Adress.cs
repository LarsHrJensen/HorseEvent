using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Domain.ValueObjects
{
    public class Adress
    {
        public string StreetName { get; }
        public string HouseNumber { get; }
        public string? Apartment { get; }  // Optional
        public string PostalCode { get; }
        public string City { get; }
        public string CountryCode { get; }
        public string CountryName { get; }

        public Adress(
            string streetName,
            string houseNumber,
            string postalCode,
            string city,
            string countryCode,
            string countryName,
            string? apartment = null)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            Apartment = apartment;
            PostalCode = postalCode;
            City = city;
            CountryCode = countryCode;
            CountryName = countryName;
        }
    }
}
