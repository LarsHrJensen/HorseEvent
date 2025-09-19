using System.ComponentModel.DataAnnotations.Schema;

namespace ClubContext.Domain.ValueObjects
{
    public class PostalCodeCity
    {
        [Column("postal_code")]
        public string PostalCode { get; }
        [Column("city")]
        public string City { get; }

        // Constructor til EF og Value Object
        public PostalCodeCity(string code, string city)
        {
            PostalCode = code;
            City = city;
        }

        private PostalCodeCity() { } // for EF
    }
}
