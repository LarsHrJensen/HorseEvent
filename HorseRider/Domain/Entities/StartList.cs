using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Domain.Entities
{
    public class StartList
    {
        public int StartListId { get; set; }
        public DateTime StartTime { get; set; }
        public int StartNumber { get; set; }
        public int ClassId { get; set; }
        public int CombinationId { get; set; }
    }
}
