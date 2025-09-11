using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Rider
    {
        public string RiderName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }

        public Rider(string name, string email, int birthYear)
        {
            RiderName = name;
            Email = email;
            BirthYear = birthYear;
        }
        public Rider() { }
    }
}
