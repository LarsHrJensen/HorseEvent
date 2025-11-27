using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRiderContext.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Application.Handlers
{
    internal class GetHorseByIdHandler : IRequestHandler<GetHorseByIdQuery, HorseDTO>
    {
            
        private readonly IHorseRepository _horseRepository;

        public GetHorseByIdHandler(IHorseRepository _horseRepository)
        {
            this._horseRepository = _horseRepository;
        }

        public async Task<HorseDTO> Handle(GetHorseByIdQuery request, CancellationToken cancellationToken)
        {
            var horse = await _horseRepository.GetByIdAsync(request.Id);

            if (horse != null)
            {
                HorseDTO horseDTO = new HorseDTO(horse);
                return horseDTO;
            }

            return null;
        }

       
    }
}
