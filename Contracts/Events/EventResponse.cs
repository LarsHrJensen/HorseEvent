using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contracts.Events
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ClubId { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public int? ClubDistrictId { get; set; }
        public string Level { get; set; } = "E";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EntryDeadline { get; set; }
        public string Status { get; set; } = "draft";

        public List<EventClassResponse> Classes { get; set; } = new();
    }

    public class EventClassResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Level { get; set; } = "E";
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; } = string.Empty;
        public int ClassLevelId { get; set; }
        public string ClassLevelName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int? MaxParticipants { get; set; }
    }
}