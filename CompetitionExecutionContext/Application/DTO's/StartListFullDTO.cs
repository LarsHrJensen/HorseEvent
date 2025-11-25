using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionExecutionContext.application.DTOs
{
    public class StartListFullDTO
    {
        public int StartListId { get; set; }
        public string CombinationName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public int StartNumber { get; set; }
        public DateTime StartTime { get; set; }
        public string RiderName { get; set; } = string.Empty;
        public string HorseName { get; set; } = string.Empty;
        public string CompetitionName { get; set; } = string.Empty;
        public string ClassLevel { get; set; } = string.Empty;
        public string DisciplineName { get; set; } = string.Empty;
    }
}
