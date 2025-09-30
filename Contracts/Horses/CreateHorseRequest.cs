namespace Contracts.Horses
{
    public class CreateHorseRequest
    {
        public string Name { get; set; }
        public string HorseId { get; set; }
        public int Height { get; set; }
        public int BirthYear { get; set; }
    }
}