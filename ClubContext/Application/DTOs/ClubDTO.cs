using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.DTOs
{
   
    public class ClubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string? Apartment { get; set; }  // Optional
        public string PostalCode { get; set; }
        public string City { get; set; }          // Bemærk! Fra PostalCodeCity VO
        public string CountryCode { get; set; }
        public string CountryName { get; set; }   // Fra Country VO
    }
    
}
