using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.DTO_s
{
    public class TopRiderDTO
    {
        public string RiderName { get; set; }
        public decimal? AverageScore { get; set; }
        public decimal? TotalScore { get; set; }
        public int Competitions { get; set; }

    }
}
