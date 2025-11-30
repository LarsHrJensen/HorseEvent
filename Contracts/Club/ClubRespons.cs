using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Club
{
    public class ClubResponse
    {
        public int ClubId { get; set; }
        public string Name { get; set; }
        public int? DistrictId { get; set; }
        public AddressDto Address { get; set; }

        public class AddressDto
        {
            public string StreetName { get; set; }
            public string StreetNumber { get; set; }
            public string PostalCode { get; set; }
            public string CountryCode { get; set; }
            public string City { get; set; }
        }
    }
}
