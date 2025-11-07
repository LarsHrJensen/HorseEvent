using EventSchedulingContext.Domain.Entities;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Application.Interfaces
{
    public interface IEventRepository: ICrudRepository<Event>
    {
    }
}
