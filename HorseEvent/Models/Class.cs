namespace HorseEvent.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public int DisciplineId { get; set; }
        public string ClassLevel { get; set; }
        public decimal? Height { get; set; }
        public string ProgramName { get; set; }
        public DateTime ClassDate { get; set; }
        public int? CompetitionId { get; set; }
    }
}
