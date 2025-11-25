using SharedKernel;
using SharedKernel.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Interfaces
{
    public interface IOutboxRepository: ICreateRepository<OutboxEvent>
    {
    }
}
