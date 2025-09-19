using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.DTOs
{
    public class PostalCodeDTO
    {
        public string PostalCode { get; set; }
        public string City { get; set; }           
        public string? Municipality { get; set; }   // fx Bourg-en-Bresse
        public string? State { get; set; }          // fx Auvergne-Rhône-Alpes

        // DisplayName kombinerer postnummer med bedste navn
        public string DisplayName =>
            !string.IsNullOrEmpty(City) ? $"{PostalCode} – {City}" :
            !string.IsNullOrEmpty(Municipality) ? $"{PostalCode} – {Municipality}" :
            !string.IsNullOrEmpty(State) ? $"{PostalCode} – {State}" :
            PostalCode;
    }
}
