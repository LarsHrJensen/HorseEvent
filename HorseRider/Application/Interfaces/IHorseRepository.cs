using HorseRider.Domain.Entities;
using SharedKernel.Interfaces;

namespace HorseRider.Application.Interfaces
{
    public interface IHorseRepository : ICrudRepository<Horse>
    {
    }
}
