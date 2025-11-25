using EventSchedulingContext.Application.DTOs;
using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Domain.Entities;

namespace EventSchedulingContext.Application.Services
{
    public class EventService : IEventService
    {
        IEventRepository _eventRepository;
        IClubReadRepository _clubReadRepository;

        public EventService(IEventRepository eventRepository, IClubReadRepository clubReadRepository)
        {
            _eventRepository = eventRepository;
            _clubReadRepository = clubReadRepository;
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO dto)
        {
            var entity = new Event
            {
                Name = dto.Name,
                ClubId = dto.ClubId,
                Level = dto.Level,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                EntryDeadline = dto.EntryDeadline,
                Status = MapStatus(dto.Status),
                Classes = dto.Classes.Select(c => new Class
                {
                    Name = c.Name,
                    Level = c.Level,
                    DisciplineId = c.DisciplineId!.Value,
                    ClassLevelId = c.ClassLevelId!.Value,
                    Date = c.Date,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants
                }).ToList()
            };

            await _eventRepository.AddAsync(entity);

            // Map tilbage til DTO
            return new EventDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                ClubId = entity.ClubId,
                Level = entity.Level,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                EntryDeadline = entity.EntryDeadline,
                Status = MapStatus(entity.Status),
                Classes = entity.Classes.Select(c => new ClassDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = c.Level,
                    DisciplineId = c.DisciplineId,
                    ClassLevelId = c.ClassLevelId,
                    Date = c.Date,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants,
                    EventId = c.EventId
                }).ToList()
            };
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();

            // Hent alle klubber i én query
            var clubs = await _clubReadRepository.GetAllAsync();

            // Lav dictionary for hurtig opslag
            var clubDict = clubs.ToDictionary(c => c.ClubId);

            return events.Select(e => new EventDTO
            {
                Id = e.Id,
                Name = e.Name,
                ClubId = e.ClubId,

                ClubDistrictId = clubDict.TryGetValue(e.ClubId, out var club)
                        ? club.DistrictId
                        : (int?)null,

                ClubName = clubDict.TryGetValue(e.ClubId, out var club2)
                        ? club2.Name
                        : "Ukendt klub",
                Level = e.Level,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                EntryDeadline = e.EntryDeadline,
                Status = MapStatus(e.Status),

                Classes = e.Classes.Select(c => new ClassDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = c.Level,
                    DisciplineId = c.DisciplineId,
                    ClassLevelId = c.ClassLevelId,
                    Date = c.Date,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants,
                    EventId = c.EventId
                }).ToList()
            });
        }

        private EventSchedulingContext.Domain.Entities.EventStatus MapStatus(EventSchedulingContext.Application.DTOs.EventStatus dtoStatus)
        {
            return dtoStatus switch
            {
                EventSchedulingContext.Application.DTOs.EventStatus.Draft => EventSchedulingContext.Domain.Entities.EventStatus.Draft,
                EventSchedulingContext.Application.DTOs.EventStatus.Open => EventSchedulingContext.Domain.Entities.EventStatus.Open,
                EventSchedulingContext.Application.DTOs.EventStatus.Closed => EventSchedulingContext.Domain.Entities.EventStatus.Closed,
                EventSchedulingContext.Application.DTOs.EventStatus.Cancelled => EventSchedulingContext.Domain.Entities.EventStatus.Cancelled,
                _ => throw new ArgumentOutOfRangeException(nameof(dtoStatus), dtoStatus, null)
            };
        }
        private EventSchedulingContext.Application.DTOs.EventStatus MapStatus(EventSchedulingContext.Domain.Entities.EventStatus domainStatus)
        {
            return domainStatus switch
            {
                EventSchedulingContext.Domain.Entities.EventStatus.Draft => EventSchedulingContext.Application.DTOs.EventStatus.Draft,
                EventSchedulingContext.Domain.Entities.EventStatus.Open => EventSchedulingContext.Application.DTOs.EventStatus.Open,
                EventSchedulingContext.Domain.Entities.EventStatus.Closed => EventSchedulingContext.Application.DTOs.EventStatus.Closed,
                EventSchedulingContext.Domain.Entities.EventStatus.Cancelled => EventSchedulingContext.Application.DTOs.EventStatus.Cancelled,
                _ => throw new ArgumentOutOfRangeException(nameof(domainStatus), domainStatus, null)
            };
        }

    }
}
