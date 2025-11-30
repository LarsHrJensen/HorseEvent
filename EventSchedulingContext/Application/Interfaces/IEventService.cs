using EventSchedulingContext.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<EventDTO> CreateEventAsync(EventDTO eventDTO);
        Task<EventDTO> GetByIdAsync(int id);
    }
}
