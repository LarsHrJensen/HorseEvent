using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.DTO_s
{
    public class ConsistencyRiderDTO
    {
        public string RiderName { get; set; } = string.Empty;
        public decimal? AverageScore { get; set; }
        public decimal? Consistency { get; set; }   // Standard deviation (jo lavere, jo mere stabil)
    }
}

