using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class EventListItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ClubId { get; set; }

        public string Level { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
