using Contracts.Events;
using EventSchedulingContext.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Mapper
{
    public static class EventMapper
    {
        public static EventDTO MapToDTO(CreateEventRequest request)
        {
            return new EventDTO
            {
                Id = null, // Ved create er der intet Id endnu
                Name = request.Name,
                ClubId = request.ClubId,
                Level = request.Level,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EntryDeadline = request.EntryDeadline,
                Status = MapStatus(request.Status),

                Classes = request.Classes.Select(c => new ClassDTO
                {
                    Id = null,
                    Name = c.Name,
                    Level = c.Level,
                    DisciplineId = c.Discipline,
                    ClassLevelId = c.ClassLevel,
                    Date = c.Date,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants
                }).ToList()
            };
        }

        private static EventStatus MapStatus(string status)
        {
            return status.ToLower() switch
            {
                "draft" => EventStatus.Draft,
                "open" => EventStatus.Open,
                "closed" => EventStatus.Closed,
                "cancelled" => EventStatus.Cancelled,
                _ => throw new ArgumentException($"Invalid event status: {status}")
            };
        }
    }

}
