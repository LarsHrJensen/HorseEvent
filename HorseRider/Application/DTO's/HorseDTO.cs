namespace HorseRider.Application.DTO_s
{
    public class HorseDTO
    {
        public int? Id { get; set; }
        public string HorseName { get; set; } = string.Empty;
        public string UELN { get; set; } = string.Empty;
        public int HorseHeight { get; set; }
        public int BirthYear { get; set; }
    }
}
