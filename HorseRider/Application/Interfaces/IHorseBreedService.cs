using HorseRiderContext.Application.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.Interfaces
{
    public interface IHorseBreedService
    {
        Task<IEnumerable<HorseBreedDTO>> GetAllHorseBreedsAsync();
    }
}
