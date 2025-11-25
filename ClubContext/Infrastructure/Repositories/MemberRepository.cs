using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;

namespace ClubContext.Infrastructure.Repositories
{
    internal class MemberRepository : IMemberRepository

    {
        public Task<Member> AddAsync(Member entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Member entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Member>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Member?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Member entity)
        {
            throw new NotImplementedException();
        }
    }
}
