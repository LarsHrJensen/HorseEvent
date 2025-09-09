namespace Contracts
{
    public class CreateHorseRequest
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Height { get; set; }
        public int BirthYear { get; set; }
    }
}