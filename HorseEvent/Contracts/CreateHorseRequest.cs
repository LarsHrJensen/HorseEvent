namespace Contracts
{
    public class CreateHorseRequest
    {
        public string Name { get; set; }
        public int HorseId { get; set; }
        public int Height { get; set; }
        public int BirthYear { get; set; }
    }
}