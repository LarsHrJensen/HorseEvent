namespace HorseEvent.Models
{
    public class Combination
    {
        public int CombinationId { get; set; }
        public string CombinationStatus { get; set; }
        public string Comment { get; set; }
        public int RiderId { get; set; }
        public int HorseId { get; set; }
    }
}
