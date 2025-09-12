using HorseRider.Application.DTO_s;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Application.Queries
{
    public record GetRidersQuery() : IRequest<List<RiderDTO>>;
}
