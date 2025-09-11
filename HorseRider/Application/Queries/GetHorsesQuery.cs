using HorseRider.Application.DTO_s;
using MediatR;

namespace HorseRider.Application.Queries
{
    public record GetHorsesQuery() : IRequest<List<HorseDTO>>;
}
