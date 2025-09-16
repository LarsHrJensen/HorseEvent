using HorseRider.Application.DTO_s;
using MediatR;

namespace HorseRider.Application.Commands
{
    public record CreateRiderCommand(string Name, string email, int BirthYear)
     : IRequest<RiderDTO>;
}

