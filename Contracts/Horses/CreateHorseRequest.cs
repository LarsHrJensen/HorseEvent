namespace Contracts.Horses
{
    public class CreateHorseRequest
    {
        // Obligatoriske felter
        public string Name { get; set; }
        public string HorseId { get; set; }
        public int Height { get; set; }
        public int BirthYear { get; set; }


        public string? Gender { get; set; } // Hoppe, Vallak, Hingst

        // Frivillige felter
        public string? Color { get; set; }
        public int? Breed { get; set; }       // Race / Avlsforbund
        public string? Breeder { get; set; }     // Avler
        public int? SireId { get; set; } = null; // Far
        public int? DamId { get; set; } = null;     // Mor
    }
}