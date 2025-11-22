namespace ClubContext.Domain.Entities
{
    public class District
    {
        public int DistrictId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set;  }
    }
}