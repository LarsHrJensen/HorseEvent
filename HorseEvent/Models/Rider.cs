namespace HorseEvent.Models
{
    public class Rider
    {
        public string RiderId { get; set; }
        public string? RiderName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool MembershipStatus { get; set; }
        public string? DRFLicenseNr { get; set; }
    }
}
