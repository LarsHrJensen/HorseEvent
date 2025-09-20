namespace HorseEvent.Models
{
    public class CourseMap
    {
        public int CourseMapId { get; set; }
        public string JumpSequence { get; set; }
        public TimeSpan? MaxTime { get; set; }
        public DateTime MapTimeStamp { get; set; }
        public int ClassId { get; set; }
    }
}
