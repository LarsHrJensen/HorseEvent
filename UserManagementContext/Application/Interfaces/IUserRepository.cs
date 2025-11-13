using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Application.Interfaces
{
    public interface IUserRepository: ICrudRepository<UserEntity>
    {
    }
}
