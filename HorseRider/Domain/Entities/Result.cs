using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Result
    {
        public int ResultId { get; set; }
        public decimal? Score { get; set; }
        public TimeSpan? ResultTime { get; set; }
        public int? Faults { get; set; }
        public int? Placements { get; set; }
        public int CombinationId { get; set; }
        public int ClassId { get; set; }
    }
}
