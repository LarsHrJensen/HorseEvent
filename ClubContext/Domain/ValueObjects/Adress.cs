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
        public PostalCodeCity PostalCodeCity { get; }
        public Country Country { get; }
    }
}
