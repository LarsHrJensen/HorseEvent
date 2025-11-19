using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Interfaces;
using HorseRider.Domain.Entities;

namespace HorseRiderContext.Application.Interfaces
{
   public interface IStartListRepository : ICrudRepository<StartList>
    {
    }
}
