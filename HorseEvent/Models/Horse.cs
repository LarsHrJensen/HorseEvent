namespace HorseEvent.Models
{
    public class Horse
    {
        public int HorseId { get; set; }
        public string HoreseUELN { get; set; }
        public string? HorseName { get; set; }
        public decimal Height { get; set; }
        public int BirthYear { get; set; }
        public string? Category { get; set; }
    }
}
