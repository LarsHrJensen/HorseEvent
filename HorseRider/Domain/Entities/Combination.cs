using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class Combination
    {
        public int CombinationId { get; set; }
        public string CombinationStatus { get; set; } = null!;
        public string? Comment { get; set; }
        public int RiderId { get; set; }
        public int HorseId { get; set; }
    }
}
