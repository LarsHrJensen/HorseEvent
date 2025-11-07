using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class CreateEventRequest
    {
        public string Name { get; set; } = string.Empty;

        public int ClubId { get; set; }

        /// <summary>
        /// Stævneniveau (f.eks. E, D, C). Indtil videre kun E.
        /// </summary>
        public string Level { get; set; } = "E";

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EntryDeadline { get; set; }

        /// <summary>
        /// Status: draft, open, closed, cancelled
        /// </summary>
        public string Status { get; set; } = "draft";

        public List<EventClassRequest> Classes { get; set; } = new();
    }

    public class EventClassRequest
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Stævnets niveau – normalt arvet fra eventet (E).
        /// </summary>
        public string Level { get; set; } = "E";

        /// <summary>
        /// FK til Discipline (Dressur, Springning, etc.)
        /// </summary>
        public int Discipline { get; set; }

        /// <summary>
        /// FK til ClassLevel (eks. LD1, LC2, LB etc.)
        /// </summary>
        public int ClassLevel { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public int? MaxParticipants { get; set; }
    }
}
