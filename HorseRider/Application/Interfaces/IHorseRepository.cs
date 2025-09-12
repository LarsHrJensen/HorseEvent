using HorseRider.Domain.Entities;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Application.Interfaces
{
    public interface IHorseRepository : ICrudRepository<Horse>
    {
    }
}
