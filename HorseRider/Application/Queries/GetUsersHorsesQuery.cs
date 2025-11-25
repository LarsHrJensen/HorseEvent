using HorseRider.Application.DTO_s;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.Queries
{
    public record GetUsersHorsesQuery() : IRequest<List<HorseDTO>>;
}
