using HorseRider.Domain.Entities;
using SharedKernel.Interfaces;

namespace HorseRider.Application.Interfaces
{
    public interface IRiderRepository : ICrudRepository<Rider>
    {
    }
}
