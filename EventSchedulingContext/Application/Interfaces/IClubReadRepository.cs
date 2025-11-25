using EventSchedulingContext.ReadModels;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Interfaces
{
    public interface IClubReadRepository : ICrudRepository<ClubReadModel>
    {
    }
}
