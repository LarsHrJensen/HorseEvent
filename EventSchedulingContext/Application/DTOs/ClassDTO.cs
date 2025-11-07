using EventSchedulingContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.DTOs
{
    public class ClassDTO
    {
        public int? Id { get; set; } // Primærnøgle
        public string Name { get; set; }

        // Samme niveau som event (fx "E")
        public string Level { get; set; }

        // Relation til Discipline
        public int? DisciplineId { get; set; }

        // Relation til ClassLevel
        public int? ClassLevelId { get; set; }

        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int? MaxParticipants { get; set; }

        // Relation til Event
        public int EventId { get; set; }
    }
}
