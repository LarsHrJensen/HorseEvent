using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces.Base
{
    public interface IUpdateRepository<T> where T : class
    {
        Task UpdateAsync(T entity);
    }
}
