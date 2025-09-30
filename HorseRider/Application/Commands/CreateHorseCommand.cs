using HorseRider.Application.DTO_s;
using MediatR;

namespace HorseRider.Application.Commands
{
    public record CreateHorseCommand(string Name, string Id, int Height, int BirthYear)
          : IRequest<HorseDTO>;
    
}
