using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.DTO_s
{
    public class RiderPerformanceItem
    {
        // Rider
        public int RiderId { get; set; }
        public string RiderName { get; set; } = string.Empty;
        public string? DRFLicenseNr { get; set; }

        // Horse
        public int HorseId { get; set; }
        public string HorseName { get; set; } = string.Empty;

        // Competition
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; } = string.Empty;
        public DateTime CompetitionStart { get; set; }
        public DateTime CompetitionEnd { get; set; }

        // Class
        public int ClassId { get; set; }
        public string ClassLevel { get; set; } = string.Empty;
        public DateTime ClassDate { get; set; }
        public string? ProgramName { get; set; }
        public int? Height { get; set; }

        // Discipline
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; } = string.Empty;

        // StartList (nullable)
        public int? StartListId { get; set; }
        public int? StartNumber { get; set; }
        public DateTime? StartTime { get; set; }

        // Result (nullable)
        public int? ResultId { get; set; }
        public decimal? Score { get; set; }
        public int? Faults { get; set; }
        public TimeSpan? ResultTime { get; set; }
        public int? Placements { get; set; }
    }
}
