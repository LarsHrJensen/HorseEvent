using EventSchedulingContext.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Interfaces
{
    public interface IDisciplineService
    {
        Task<IEnumerable<DisciplinDTO>> GetAllDisciplinesAsync();
    }
}
