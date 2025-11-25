using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.ReadModels
{
    public class ClubReadModel
    {
        public int ClubId { get; set; }
        public string Name { get; set; }
        public int? DistrictId { get; set; }
    }
}
