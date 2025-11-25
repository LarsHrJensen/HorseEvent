using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Class
    {
        public int ClassId { get; set; }

        // Fremmednøgler
        public int CompetitionId { get; set; }
        public int DisciplineId { get; set; }

        // Egenskaber
        public string ClassLevel { get; set; } = null!;
        public int? Height { get; set; }  // Nullable, da det kan være NULL i DB
        public string? ProgramName { get; set; } // Nullable
        public DateTime ClassDate { get; set; }

    }
}
