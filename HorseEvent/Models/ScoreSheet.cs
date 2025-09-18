namespace HorseEvent.Models
{
    public class ScoreSheet
    {
        public int ScoreSheetId { get; set; }
        public string Comments { get; set; }
        public string ExeciseScores { get; set; }
        public decimal? TotalPercentage { get; set; }
        public int ResultId { get; set; }
    }
}
