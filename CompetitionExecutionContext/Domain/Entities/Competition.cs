using HorseRider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionExecutionContext.Domain.Entities
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = null!;

        // Liste over klasser til konkurrencen
        public List<Class> Classes { get; set; } = new List<Class>();
    }
}
