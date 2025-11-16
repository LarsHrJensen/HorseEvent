using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Horse
    {
        // Primary Key
        public int HorseId { get; set; }

        // Required
        public string Name { get; set; } = string.Empty;
        public string UELN { get; set; } = string.Empty;  // Universal Equine Life Number
        public int Height { get; set; }
        public int BirthYear { get; set; }
        public string? Gender { get; set; } = string.Empty;   // Hoppe, Vallak, Hingst

        // Optional fields
        public string? Color { get; set; }
        public string? Breed { get; set; }       // Race / Avlsforbund
        public string? Breeder { get; set; }     // Avler
        public int? SireId { get; set; }      // far
        public int? DamId { get; set; }      //  mor

        // Derived property (Larman: “Derived attributes should not be stored”)
        public string Category =>
            Height <= 130 ? "Kat 3" :
            Height <= 140 ? "Kat 2" :
            Height <= 148 ? "Kat 1" : "Hest";

        public Horse(
            string name,
            string ueln,
            int height,
            int birthYear,
            string sex,
            string? color = null,
            string? breed = null,
            string? breeder = null,
            int? father = null,
            int? mother = null)
        {
            Name = name;
            UELN = ueln;
            Height = height;
            BirthYear = birthYear;
            Gender = sex;
            Color = color;
            Breed = breed;
            Breeder = breeder;
            SireId = father;
            DamId = mother;
        }

        public Horse(string name, string ueln, int height, int birthYear)
        {
            Name = name;
            UELN = ueln;
            Height = height;
            BirthYear = birthYear;
        }
  
        public Horse() { }
    }
}


