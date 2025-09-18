namespace HorseEvent.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string Comments { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime? RegistrationTime { get; set; }
        public int CombinationId { get; set; }
        public int ClassId { get; set; }
    }
}
