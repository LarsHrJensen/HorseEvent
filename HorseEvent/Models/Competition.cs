namespace HorseEvent.Models
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }

        public List<Class> Classes { get; set; } = new ();
    }
}
