using SharedKernel.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces
{
    public interface ICrudRepository<T> : IReadRepository<T>, ICreateRepository<T>, IUpdateRepository<T>, IDeleteRepository<T> where T : class
    {
    }
}
