namespace Contracts.Horses
{
    public class HorseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UELN { get; set; }
        public int Height { get; set; }
        public int BirthYear { get; set; }
        public string Category { get; set; }
        public string Gender { get; set;  }

        public string? Color { get; set; }
        public string? BreedName { get; set; }       // Race / Avlsforbund
        public int? BreedId { get; set; }
        public string? Breeder { get; set; }     // Avler
        public int? SireId { get; set; } = null; // Far
        public int? DamId { get; set; } = null;
    }
}