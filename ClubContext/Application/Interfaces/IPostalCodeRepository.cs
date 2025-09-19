using ClubContext.Domain.ValueObjects;
using SharedKernel.Interfaces.Base;

namespace ClubContext.Application.Interfaces
{
    public interface IPostalCodeRepository : IReadRepository<PostalCodeCity>
    {
    }
}
