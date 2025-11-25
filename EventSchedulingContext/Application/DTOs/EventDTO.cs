using EventSchedulingContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.DTOs
{
    public class EventDTO
    {
        public int? Id { get; set; } // Primærnøgle
        public string Name { get; set; }

        // Relation til Club
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int? ClubDistrictId { get; set;  }

        // Kun "E" niveau for nu
        public string Level { get; set; } = "E";

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EntryDeadline { get; set; }

        // Status enum kunne også være en god ide
        public EventStatus Status { get; set; }

        // Relation til klasser
        public List<ClassDTO> Classes { get; set; } = new();
    }

    public enum EventStatus
    {
        Draft,
        Open,
        Closed,
        Cancelled
    }
}
