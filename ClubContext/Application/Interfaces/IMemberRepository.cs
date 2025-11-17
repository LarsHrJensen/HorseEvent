using ClubContext.Domain.Entities;
using SharedKernel.Interfaces;

namespace ClubContext.Application.Interfaces
{
    public interface IMemberRepository : ICrudRepository<Member>
    {
    
    }
}
