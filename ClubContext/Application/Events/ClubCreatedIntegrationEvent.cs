using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Events
{
    public record ClubCreatedIntegrationEvent
    {
        public int ClubId { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
        public string Country { get; init; }

        public ClubCreatedIntegrationEvent(int clubId, string name, string city, string country)
        {
            ClubId = clubId;
            Name = name;
            City = city;
            Country = country;
        }
    }
}

