using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.DTO_s
{
    public class RiderHorsePerformanceDTO
    {
        public string RiderName { get; set; }
        public string HorseName { get; set; }
        public decimal? AvgScore { get; set; }
        public decimal? DiffFromHorseAvg { get; set; }
    }
}
