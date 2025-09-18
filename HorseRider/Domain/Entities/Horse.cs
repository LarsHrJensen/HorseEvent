using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Horse
    {
        public int HorseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UELN { get; set; } //Universal Equine Life Number
        public int Height { get; set; }
        public int BirthYear { get; set; }
        public string Category => Height <= 130 ? "Kat 3" :
                            Height <= 140 ? "Kat 2" :
                            Height <= 148 ? "Kat 1" : "Hest";

        public Horse(string name, string ueln, int height, int birthYear)
        {
            Name = name;
            UELN = ueln;
            Height = height;
            BirthYear = birthYear;
        }
        public Horse(int id, string name, string ueln, int height, int birthYear)
        {
            HorseId = id;
            Name = name;
            UELN = ueln;
            Height = height;
            BirthYear = birthYear;
        }
        public Horse() { }
    }
}


