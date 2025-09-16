using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Horse
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public int Height { get; set; }
        public int BirthYear { get; set; }

        public Horse(string name, string id, int height, int birthYear)
        {
            Name = name;
            Id = id;
            Height = height;
            BirthYear = birthYear;
        }
    }
}


