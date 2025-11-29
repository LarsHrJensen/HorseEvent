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
                Id = null, // Ved create er der intet ClubId endnu
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
                    DisciplineId = c.DisciplineId,
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

        public static EventResponse MapToResponse(EventDTO dto)
        {
            return new EventResponse
            {
                Id = dto.Id ?? 0,
                Name = dto.Name ?? string.Empty,
                ClubId = dto.ClubId,
                ClubName = dto.ClubName ?? string.Empty,
                ClubDistrictId = dto.ClubDistrictId,
                Level = dto.Level,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                EntryDeadline = dto.EntryDeadline,
                Status = dto.Status.ToString().ToLower(),
                Classes = dto.Classes?.Select(c => new EventClassResponse
                {
                    Id = c.Id ?? 0,
                    Name = c.Name ?? string.Empty,
                    Level = c.Level ?? "E",
                    DisciplineId = (int)c.DisciplineId,
                    ////DisciplineName = c.Di ?? string.Empty,
                    //ClassLevelId = c.ClassLevelId,
                    //ClassLevelName = c.ClassLevelName ?? string.Empty,
                    Date = c.Date,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants
                }).ToList() ?? new List<EventClassResponse>()
            };
        }
    }

}
