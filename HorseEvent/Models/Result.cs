namespace HorseEvent.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public decimal? Score { get; set; }
        public TimeSpan? ResultTime { get; set; }
        public int? Faults { get; set; }
        public int? Placement { get; set; }
        public int CombinationId { get; set; }
        public int ClassId { get; set; }
    }
}
