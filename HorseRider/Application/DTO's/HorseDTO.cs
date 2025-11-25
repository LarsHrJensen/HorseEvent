using HorseRider.Domain.Entities;
using HorseRiderContext.Domain.Entities;

namespace HorseRider.Application.DTO_s
{
    public class HorseDTO
    {
        public int? Id { get; set; }
        public string HorseName { get; set; } = string.Empty;
        public string UELN { get; set; } = string.Empty;
        public int HorseHeight { get; set; }
        public int BirthYear { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }

        // Optional fields
        public string? Color { get; set; }
        public int? BreedId { get; set; }       // Race / Avlsforbund
        public string? BreedName { get; set; }
        public string? Breeder { get; set; }     // Avler
        public int? SireId { get; set; }      // far
        public int? DamId { get; set; }      //  mor

        public HorseDTO(Horse horse)
        {
            Id = horse.HorseId;
            HorseName = horse.Name;
            UELN = horse.UELN;
            HorseHeight = horse.Height;
            BirthYear = horse.BirthYear;
            Gender = horse.Gender ?? string.Empty;
            Category = horse.Category;
            Color = horse.Color;
            BreedId = horse.BreedId;
            BreedName = horse.Breed?.Name;
            Breeder = horse.Breeder;
            SireId = horse.SireId;
            DamId = horse.DamId;
        }
        public HorseDTO()
        {
        }
    }
}
