namespace Contracts.Club
{
    public class CreateClubRequest
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public int? District { get; set; }
    }
    public class AddressDto
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string City { get; set; }
    }
}
