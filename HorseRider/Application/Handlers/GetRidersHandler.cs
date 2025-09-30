using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Application.Handlers
{
    internal class GetRidersHandler : IRequestHandler<GetRidersQuery, List<RiderDTO>>
    {
        private readonly IRiderRepository _riderRepository;
        public GetRidersHandler(IRiderRepository _riderRepository)
        {
            this._riderRepository = _riderRepository;
        }
        public async Task<List<RiderDTO>> Handle(GetRidersQuery request, CancellationToken cancellationToken)
        {
            var riders = await _riderRepository.GetAllAsync();
            return riders.Select(r => new RiderDTO
            {
                RiderName = r.RiderName,
                Id = r.Id,
                BirthYear = r.BirthYear
            }).ToList();
        }
    }
}
